using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Responses.Doctor.Get;
using diet_server_api.DTO.Responses.Patient;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly mdzcojxmContext _dbContext;

        public AppointmentsService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<DateTime>> GetDoctorAppointmentDates(int idDoctor)
        {
            var results = await _dbContext.Visits.Where(e => e.Iddoctor == idDoctor).Select(e => e.Date).Distinct().OrderBy(e => e.Date).ToListAsync();
            if (results.Count == 0) throw new NotFound("No results");
            return results;
        }

        public async Task<GetDoctorAppointmentDetailsResponse> GetDoctorAppointmentDetails(int idVisit)
        {
            var visitExists = await _dbContext.Visits.AnyAsync(e => e.Idvisit == idVisit);
            if (!visitExists) throw new NotFound("Visit not found");
            var visit = await _dbContext.Visits
            .Include(e => e.IddoctorNavigation).ThenInclude(e => e.IduserNavigation)
            .Include(e => e.IdpatientNavigation).ThenInclude(e => e.IduserNavigation)
            .Where(e => e.Idvisit == idVisit)
            .Select(e => new GetDoctorAppointmentDetailsResponse
            {
                PatientFullName = $"{e.IdpatientNavigation.IduserNavigation.Firstname} {e.IdpatientNavigation.IduserNavigation.Lastname}",
                Email = e.IdpatientNavigation.IduserNavigation.Email,
                DateOfBirth = e.IdpatientNavigation.IduserNavigation.Dateofbirth.ToString(),
                Date = e.Date.ToString(),
                Time = ((int)e.Time.TotalHours + e.Time.ToString(@"\:mm\:ss")).Substring(0, 5),
                Description = e.Description
            }).FirstOrDefaultAsync();
            return visit;
        }

        public async Task<List<GetDoctorAppointmentsByDateResponse>> GetDoctorAppointmentsByDates(DateTime date, int idDoctor)
        {
            var visits = await _dbContext.Visits
            .Include(e => e.IddoctorNavigation).ThenInclude(e => e.IduserNavigation)
            .Include(e => e.IdpatientNavigation).ThenInclude(e => e.IduserNavigation)
            .Where(e => e.Date.Date == date.Date && e.Iddoctor == idDoctor)
            .OrderBy(e => e.Time)
            .Select(e => new GetDoctorAppointmentsByDateResponse
            {
                IdVisit = e.Idvisit,
                PatientFullName = $"{e.IdpatientNavigation.IduserNavigation.Firstname} {e.IdpatientNavigation.IduserNavigation.Lastname}",
                TimeToDisplay = ((int)e.Time.TotalHours + e.Time.ToString(@"\:mm\:ss")).Substring(0, 5),
                Date = e.Date
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
                .Where(e => e.Date.Date == closestDate.Date && e.Iddoctor == idDoctor)
                .OrderBy(e => e.Time)
                .Select(e => new GetDoctorAppointmentsByDateResponse
                {
                    IdVisit = e.Idvisit,
                    PatientFullName = $"{e.IdpatientNavigation.IduserNavigation.Firstname} {e.IdpatientNavigation.IduserNavigation.Lastname}",
                    TimeToDisplay = ((int)e.Time.TotalHours + e.Time.ToString(@"\:mm\:ss")).Substring(0, 5),
                    Date = e.Date
                }).ToListAsync();
            }
            if (visits.Count == 0) throw new NotFound("No results found");
            return visits;
        }

        public async Task<List<DateTime>> GetPatientAppointmentDates(int idPatient)
        {
            var results = await _dbContext.Visits.Where(e => e.Idpatient == idPatient).Select(e => e.Date).Distinct().OrderBy(e => e.Date).ToListAsync();
            if (results.Count == 0) throw new NotFound("No results");
            return results;
        }

        public async Task<GetPatientAppointmentDetailsResponse> GetPatientAppointmentDetails(int idVisit)
        {
            var visitExists = await _dbContext.Visits.AnyAsync(e => e.Idvisit == idVisit);
            if (!visitExists) throw new NotFound("Visit not found");
            var visit = await _dbContext.Visits
            .Include(e => e.IddoctorNavigation).ThenInclude(e => e.IduserNavigation)
            .Include(e => e.IdpatientNavigation).ThenInclude(e => e.IduserNavigation)
            .Where(e => e.Idvisit == idVisit)
            .Select(e => new GetPatientAppointmentDetailsResponse
            {
                DoctorFullName = $"{e.IddoctorNavigation.IduserNavigation.Firstname} {e.IddoctorNavigation.IduserNavigation.Lastname}",
                Office = e.IddoctorNavigation.Office,
                Email = e.IddoctorNavigation.IduserNavigation.Email,
                Date = e.Date.ToString(),
                Time = ((int)e.Time.TotalHours + e.Time.ToString(@"\:mm\:ss")).Substring(0, 5),
                Description = e.Description
            }).FirstOrDefaultAsync();
            return visit;
        }

        public async Task<List<GetPatientAppointmentsByDateResponse>> GetPatientAppointmentsByDates(int idPatient)
        {
            var visits = await _dbContext.Visits
            .Include(e => e.IddoctorNavigation).ThenInclude(e => e.IduserNavigation)
            .Include(e => e.IdpatientNavigation).ThenInclude(e => e.IduserNavigation)
            .Where(e => e.Date.Date >= DateTime.Now.Date && e.Idpatient == idPatient)
            .OrderBy(e => e.Time)
            .Select(e => new GetPatientAppointmentsByDateResponse
            {
                IdVisit = e.Idvisit,
                DoctorFullName = $"{e.IddoctorNavigation.IduserNavigation.Firstname} {e.IddoctorNavigation.IduserNavigation.Lastname}",
                TimeToDisplay = ((int)e.Time.TotalHours + e.Time.ToString(@"\:mm\:ss")).Substring(0, 5),
                Date = e.Date
            }).ToListAsync();
            if (visits.Count == 0) throw new NotFound("No results found");
            return visits;
        }
    }
}