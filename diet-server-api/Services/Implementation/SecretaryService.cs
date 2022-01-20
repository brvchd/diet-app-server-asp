using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Secretary;
using diet_server_api.DTO.Responses.Secretary;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace diet_server_api.Services.Implementation
{
    public class SecretaryService : ISecretaryService
    {
        private readonly IConfiguration _config;
        private readonly mdzcojxmContext _dbContext;

        public SecretaryService(IConfiguration config, mdzcojxmContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        public async Task AssignAppointment(AssignAppointmentRequest request)
        {
            var doctorExists = await _dbContext.Doctors.AnyAsync(e => e.Iduser == request.IdDoctor);
            if (!doctorExists) throw new NotFound("Doctor provided not found");
            var doctorIsActive = await _dbContext.Users.AnyAsync(e => e.Iduser == request.IdDoctor && e.Isactive == true);
            if (!doctorIsActive) throw new NotActive("Account is not active");
            var patientExists = await _dbContext.Patients.AnyAsync(e => e.Iduser == request.IdPatient && e.Ispending == false);
            if (!patientExists) throw new NotFound("Patient not found");
            var patientIsActive = await _dbContext.Users.AnyAsync(e => e.Iduser == request.IdPatient && e.Isactive == true);
            if (!patientIsActive) throw new NotActive("Account is not active");
            if (request.AppointmentDate < DateTime.UtcNow) throw new InvalidData("Date is not correct");

            var appointmentTime = request.AppointmentDate.TimeOfDay;
            var alreadyBooked = await _dbContext.Visits.AnyAsync(e => e.Iddoctor == request.IdDoctor && e.Date == request.AppointmentDate && e.Time == appointmentTime);
            if (alreadyBooked) throw new AlreadyExists("There is already booked appointment for this doctor at specified date");
            var appointment = new Visit()
            {
                Iddoctor = request.IdDoctor,
                Idpatient = request.IdPatient,
                Time = appointmentTime,
                Date = request.AppointmentDate,
                Description = request.Description
            };

            await _dbContext.Visits.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CancelAppointment(int idVisit)
        {
            var visit = await _dbContext.Visits
            .FirstOrDefaultAsync(e => e.Idvisit == idVisit);
            if (visit.Time <= DateTime.UtcNow.TimeOfDay && visit.Date <= DateTime.Now) throw new InvalidData("Not possible to cancel visit");
            _dbContext.Visits.Remove(visit);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<DateTime>> GetAppointmentDates()
        {
            var results = await _dbContext.Visits.Select(e => e.Date).Distinct().OrderBy(e => e.Date).ToListAsync();
            if (results.Count == 0) throw new NotFound("No results");
            return results;
        }

        public async Task<GetAppointmentDetails> GetAppointmentDetails(int idVisit)
        {
            var visitExists = await _dbContext.Visits.AnyAsync(e => e.Idvisit == idVisit);
            if (!visitExists) throw new NotFound("Visit not found");
            var visit = await _dbContext.Visits
            .Include(e => e.IddoctorNavigation).ThenInclude(e => e.IduserNavigation)
            .Include(e => e.IdpatientNavigation).ThenInclude(e => e.IduserNavigation)
            .Where(e => e.Idvisit == idVisit)
            .Select(e => new GetAppointmentDetails
            {
                DoctorFullName = $"{e.IddoctorNavigation.IduserNavigation.Firstname} {e.IddoctorNavigation.IduserNavigation.Lastname}",
                PatientFullName = $"{e.IdpatientNavigation.IduserNavigation.Firstname} {e.IdpatientNavigation.IduserNavigation.Lastname}",
                PatientEmail = e.IdpatientNavigation.IduserNavigation.Email,
                PatientDateOfBirth = e.IddoctorNavigation.IduserNavigation.Dateofbirth.ToString(),
                AppointmentDate = e.Date.ToString(),
                AppointmentTime = ((int)e.Time.TotalHours + e.Time.ToString(@"\:mm\:ss")).Substring(0, 5),
                Description = e.Description
            }).FirstOrDefaultAsync();
            return visit;
        }

        public async Task<List<GetAppointmentsByDateResponse>> GetAppointmentsByDates(DateTime date)
        {
            var visits = await _dbContext.Visits
            .Include(e => e.IddoctorNavigation).ThenInclude(e => e.IduserNavigation)
            .Include(e => e.IdpatientNavigation).ThenInclude(e => e.IduserNavigation)
            .Where(e => e.Date.Date == date.Date)
            .OrderBy(e => e.Time)
            .Select(e => new GetAppointmentsByDateResponse
            {
                IdVisit = e.Idvisit,
                DoctorFullName = $"{e.IddoctorNavigation.IduserNavigation.Firstname} {e.IddoctorNavigation.IduserNavigation.Lastname}",
                PatientFullName = $"{e.IdpatientNavigation.IduserNavigation.Firstname} {e.IdpatientNavigation.IduserNavigation.Lastname}",
                TimeToDisplay = ((int)e.Time.TotalHours + e.Time.ToString(@"\:mm\:ss")).Substring(0, 5)
            }).ToListAsync();

            if (visits.Count == 0 && date.Date == DateTime.UtcNow.Date)
            {
                var closestDate = await _dbContext.Visits
                .Where(e => e.Date > date)
                .Select(e => e.Date)
                .FirstOrDefaultAsync();

                visits = await _dbContext.Visits
                .Include(e => e.IddoctorNavigation).ThenInclude(e => e.IduserNavigation)
                .Include(e => e.IdpatientNavigation).ThenInclude(e => e.IduserNavigation)
                .Where(e => e.Date.Date == closestDate.Date)
                .OrderBy(e => e.Time)
                .Select(e => new GetAppointmentsByDateResponse
                {
                    IdVisit = e.Idvisit,
                    DoctorFullName = $"{e.IddoctorNavigation.IduserNavigation.Firstname} {e.IddoctorNavigation.IduserNavigation.Lastname}",
                    PatientFullName = $"{e.IdpatientNavigation.IduserNavigation.Firstname} {e.IdpatientNavigation.IduserNavigation.Lastname}",
                    TimeToDisplay = ((int)e.Time.TotalHours + e.Time.ToString(@"\:mm\:ss")).Substring(0, 5)
                }).ToListAsync();
            }
            if (visits.Count == 0) throw new NotFound("No results found");
            return visits;
        }

        public async Task<List<SearchUserResponse>> SearchDoctor(string firstname, string lastname)
        {
            if (string.IsNullOrWhiteSpace(firstname))
            {
                var users = await _dbContext.Users
                    .Include(e => e.Patient)
                    .Where(e => e.Lastname.ToLower() == lastname.ToLower() && e.Role == Roles.DOCTOR)
                    .Select(e => new SearchUserResponse
                    {
                        IdUser = e.Iduser,
                        FirstName = e.Firstname,
                        LastName = e.Lastname,
                        Email = e.Email
                    }).OrderBy(e => e.FirstName).ToListAsync();
                if (users.Count == 0) throw new NotFound("No matched results");
                return users;
            }
            else
            {
                var users = await _dbContext.Users
                .Where(e => e.Firstname.ToLower() == firstname.ToLower() && e.Lastname.ToLower() == lastname.ToLower() && e.Role == Roles.DOCTOR)
                .Include(e => e.Doctor)
                .Select(e => new SearchUserResponse
                {
                    IdUser = e.Iduser,
                    FirstName = e.Firstname,
                    LastName = e.Lastname,
                    Email = e.Email
                }).OrderBy(e => e.FirstName).ToListAsync();
                if (users.Count == 0) throw new NotFound("Doctors not found");
                return users;
            }
        }

        public async Task<List<SearchUserResponse>> SearchPatient(string firstname, string lastname)
        {
            if (string.IsNullOrWhiteSpace(firstname))
            {
                var users = await _dbContext.Users
                    .Include(e => e.Patient)
                    .Where(e => e.Lastname.ToLower() == lastname.ToLower() && e.Role == Roles.PATIENT)
                    .Select(e => new SearchUserResponse
                    {
                        IdUser = e.Iduser,
                        FirstName = e.Firstname,
                        LastName = e.Lastname,
                        Email = e.Email
                    }).OrderBy(e => e.FirstName).ToListAsync();
                if (users.Count == 0) throw new NotFound("No matched results");
                return users;
            }
            else
            {
                var users = await _dbContext.Users
                .Where(e => e.Firstname.ToLower() == firstname.ToLower() && e.Lastname.ToLower() == lastname.ToLower() && e.Role == Roles.PATIENT)
                .Include(e => e.Doctor)
                .Select(e => new SearchUserResponse
                {
                    IdUser = e.Iduser,
                    FirstName = e.Firstname,
                    LastName = e.Lastname,
                    Email = e.Email
                }).OrderBy(e => e.FirstName).ToListAsync();
                if (users.Count == 0) throw new NotFound("Patients not found");
                return users;
            }
        }

        public async Task SendEmail(SendEmailRequest request)
        {
            var emailExists = await _dbContext.TempUsers.FirstOrDefaultAsync(e => e.Email.ToLower() == request.Email.ToLower().Trim());
            if (emailExists != null)
            {
                _dbContext.TempUsers.Remove(emailExists);
            }
            var tempUser = new TempUser()
            {
                Email = request.Email,
                Uniquekey = Guid.NewGuid().ToString()
            };
            await _dbContext.TempUsers.AddAsync(tempUser);
            await _dbContext.SaveChangesAsync();
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Diet Site", "dietappeu@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(request.FullName.Trim(), request.Email.ToLower().Trim()));
            emailMessage.Subject = "Temporary credentials";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<p>Dear {request.FullName},</p> <p>Your login: {request.Email}</p> <p>Your password: {tempUser.Uniquekey}</p> <p>Please proceed to a website and finish the registration process.</p> <p>Best regards,</p> <p>Site administration"
            };
            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587);
            await client.AuthenticateAsync(_config["Email"], _config["EmailPassword"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}