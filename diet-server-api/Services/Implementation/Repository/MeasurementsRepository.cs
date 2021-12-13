using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Patient;
using diet_server_api.DTO.Responses.Patient;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class MeasurementsRepository : IMeasurementRepository
    {
        private readonly mdzcojxmContext _dbContext;

        public MeasurementsRepository(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AddMeasrumentsResponse> AddMeasruments(AddMeasrumentsRequest request, string whoMeasured)
        {
            var exists = await _dbContext.Users.Include(e => e.Patient).AnyAsync(e => e.Iduser == request.Idpatient && e.Patient.Ispending == false);
            if (!exists) throw new NotFound("Patient not found");

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

        public async Task<List<GetMeasurementsResponse>> GetMeasurements(int idPatient)
        {
            var exists = await _dbContext.Patients.AnyAsync(e => e.Iduser == idPatient);
            if (!exists) throw new NotFound("Patient not found");
            var measurements = await _dbContext.Measurements.Where(e => e.Idpatient == idPatient).Select(e => new GetMeasurementsResponse
            {
                IdMeasurement = e.Idmeasurement,
                Date = e.Date
            }).OrderBy(e => e.Date).ToListAsync();
            if (measurements.Count == 0) throw new NotFound("No measurments found");
            return measurements;

        }

        public async Task<List<GetMeasurementsByDateResponse>> GetMeasurements(int idPatient, DateTime date)
        {
            var exists = await _dbContext.Patients.AnyAsync(e => e.Iduser == idPatient);
            if (!exists) throw new NotFound("Patient not found");
            if(date == null || date > DateTime.UtcNow) throw new InvalidData("Incorrect date provided"); 
            var measurements = await _dbContext.Measurements.Where(e => e.Idpatient == idPatient && e.Date == date).Select(e => new GetMeasurementsByDateResponse
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
                Whomeasured = e.Whomeasured
            }).OrderBy(e => e.Date).ToListAsync();
            if (measurements.Count == 0) throw new NotFound("No measurments found");
            return measurements;
        }
    }
}