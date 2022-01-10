using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Patient;
using diet_server_api.DTO.Responses.Patient;
using diet_server_api.Exceptions;
using diet_server_api.Helpers.Calculators;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class MeasurementsService : IMeasurementService
    {
        private readonly mdzcojxmContext _dbContext;

        public MeasurementsService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AddMeasrumentsResponse> AddMeasurements(AddMeasrumentsRequest request, string whoMeasured)
        {
            var exists = await _dbContext.Users.Include(e => e.Patient).AnyAsync(e => e.Iduser == request.Idpatient && e.Patient.Ispending == false);
            if (!exists) throw new NotFound("Patient not found");
            var measurementExists = await _dbContext.Measurements.AnyAsync(e => e.Idpatient == request.Idpatient && e.Date.Date == DateTime.UtcNow.Date && e.Whomeasured == whoMeasured);
            if (measurementExists) throw new AlreadyExists("Measurement has been already created today by you");

            var measurements = new Measurement()
            {
                Idpatient = request.Idpatient,
                Date = DateTime.UtcNow,
                Height = request.Height,
                Weight = request.Weight,
                Waistcircumference = request.Waistcircumference,
                Bicepscircumference = request.Bicepscircumference,
                Hipcircumference = request.Hipcircumference,
                Thighcircumference = request.Thighcircumference,
                Chestcircumference = request.Chestcircumference,
                Waistlowercircumference = request.Waistlowercircumference,
                Calfcircumference = request.Calfcircumference,
                Whomeasured = whoMeasured
            };

            await _dbContext.Measurements.AddAsync(measurements);
            await _dbContext.SaveChangesAsync();

            return new AddMeasrumentsResponse() { IdMeasurment = measurements.Idmeasurement, WhoMeasured = measurements.Whomeasured };
        }

        public async Task<List<GetMeasurementResponse>> GetMeasurement(int idPatient)
        {
            var patient = await _dbContext.Users.Include(e => e.Patient).Where(e => e.Iduser == idPatient).FirstOrDefaultAsync();
            var measurements = await _dbContext.Measurements.Where(e => e.Idpatient == idPatient).Select(e => new GetMeasurementResponse
            {
                Height = e.Height,
                Weight = e.Weight,
                Date = e.Date,
                Hipcircumference = e.Hipcircumference,
                Waistcircumference = e.Waistcircumference,
                Bicepscircumference = e.Bicepscircumference,
                Chestcircumference = e.Chestcircumference,
                Thighcircumference = e.Thighcircumference,
                Calfcircumference = e.Calfcircumference,
                Waistlowercircumference = e.Waistlowercircumference,
                Whomeasured = e.Whomeasured,
                Age = AgeCalculator.CalculateAge(patient.Dateofbirth),
                Gender = patient.Patient.Gender
            }).OrderByDescending(e => e.Date).Take(1).ToListAsync();
            if (measurements.Count == 0) throw new NotFound("No measurments found");
            return measurements;
        }

        public async Task<List<GetMeasurementsResponse>> GetMeasurements(int idPatient)
        {
            var exists = await _dbContext.Patients.AnyAsync(e => e.Iduser == idPatient);
            if (!exists) throw new NotFound("Patient not found");
            var measurements = await _dbContext.Measurements.Where(e => e.Idpatient == idPatient).Select(e => new GetMeasurementsResponse
            {
                IdMeasurement = e.Idmeasurement,
                Date = e.Date,
                WhoMeasured = e.Whomeasured
            }).OrderByDescending(e => e.Date).ToListAsync();
            if (measurements.Count == 0) throw new NotFound("No measurments found");
            return measurements;

        }

        public async Task<List<GetMeasurementsByDateResponse>> GetMeasurements(int idPatient, DateTime date, string whomeasured)
        {
            var patient = await _dbContext.Users.Include(e => e.Patient).Where(e => e.Iduser == idPatient).FirstOrDefaultAsync();
            if (patient == null) throw new NotFound("Patient not found");
            if (date > DateTime.UtcNow) throw new InvalidData("Incorrect date provided");
            var measurements = await _dbContext.Measurements.Where(e => e.Idpatient == idPatient && e.Date.Date == date.Date && e.Whomeasured.ToLower() == whomeasured.ToLower().Trim()).Select(e => new GetMeasurementsByDateResponse
            {
                Height = e.Height,
                Weight = e.Weight,
                Date = e.Date,
                Hipcircumference = e.Hipcircumference,
                Waistcircumference = e.Waistcircumference,
                Bicepscircumference = e.Bicepscircumference,
                Chestcircumference = e.Chestcircumference,
                Thighcircumference = e.Thighcircumference,
                Calfcircumference = e.Calfcircumference,
                Waistlowercircumference = e.Waistlowercircumference,
                Whomeasured = e.Whomeasured,
                Age = AgeCalculator.CalculateAge(patient.Dateofbirth),
                Gender = patient.Patient.Gender
            }).OrderByDescending(e => e.Date).Take(1).ToListAsync();
            if (measurements.Count == 0) throw new NotFound("No measurments found");
            return measurements;
        }
    }
}