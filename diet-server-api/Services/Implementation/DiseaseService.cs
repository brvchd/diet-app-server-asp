using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Requests.KnowledgeBase.Update;
using diet_server_api.DTO.Responses.KnowledgeBase;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.DTO.Responses.KnowledgeBase.Update;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class DiseaseService : IDiseaseService
    {
        private readonly mdzcojxmContext _dbContext;

        public DiseaseService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddDiseaseResponse> AddDisease(AddDiseaseRequest request)
        {
            var exists = await _dbContext.Diseases.AnyAsync(e => e.Name == request.Name);
            if (exists) throw new AlreadyExists("Disease already exists");
            var disease = new Disease()
            {
                Name = request.Name,
                Description = request.Description,
                Recomendation = request.Recomendation
            };
            await _dbContext.Diseases.AddAsync(disease);
            await _dbContext.SaveChangesAsync();
            return new AddDiseaseResponse()
            {
                IdDisease = disease.Iddisease,
                Name = disease.Name,
                Description = disease.Description
            };
        }

        public async Task AssignDisease(AssignDiseaseRequest request)
        {
            var patientExists = await _dbContext.Patients.Include(e => e.IduserNavigation).AnyAsync(e => e.Iduser == request.IdPatient && e.Ispending == false && e.IduserNavigation.Isactive == true);
            if (!patientExists) throw new InvalidData("Patient is either does not exist or is pending");
            var diseaseExists = await _dbContext.Diseases.AnyAsync(e => e.Iddisease == request.IdDisease);
            if (!diseaseExists) throw new NotFound("Disease not found");
            var diseasespatients = await _dbContext.DiseasePatients.ToListAsync();
            var diseaseAdded = await _dbContext.DiseasePatients.AnyAsync(e => e.Idpatient == request.IdPatient && e.Iddisease == request.IdDisease && e.Dateofcure == null);
            if (diseaseAdded) throw new AlreadyExists("Disease already added");
            var diseasePatient = new DiseasePatient()
            {
                Iddisease = request.IdDisease,
                Idpatient = request.IdPatient,
                Dateofdiagnosis = request.DateOfDiagnosis,
                Dateofcure = request.DateOfCure
            };
            await _dbContext.DiseasePatients.AddAsync(diseasePatient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAssignedDisease(int idDiseasePatient)
        {
            var disease = await _dbContext.DiseasePatients.FirstOrDefaultAsync(e => e.IddiseasePatient == idDiseasePatient);
            if (disease == null) throw new NotFound("Assigned disease not found");
            _dbContext.DiseasePatients.Remove(disease);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GetDiseasesResponse> GetDiseases(int page)
        {
            if (page < 1) page = 1;
            int pageSize = 8;
            var rows = await _dbContext.Diseases.CountAsync();
            var diseases = await _dbContext.Diseases.OrderBy(e => e.Name).Skip((page - 1) * pageSize).Take(pageSize).Select(e => new GetDiseasesResponse.Disease
            {
                IdDisease = e.Iddisease,
                Name = e.Name,
                Description = e.Description,
                Recomendation = e.Recomendation
            }).ToListAsync();

            return new GetDiseasesResponse()
            {
                Diseases = diseases,
                PageNumber = page,
                PageSize = pageSize,
                TotalRows = rows
            };
        }

        public async Task<List<GetPatientDiseasesResponse>> GetPatientDiseases(int patientId)
        {
            var patientExists = await _dbContext.Patients.Include(e => e.IduserNavigation).AnyAsync(e => e.Iduser == patientId && e.Ispending == false && e.IduserNavigation.Isactive == true);
            if (!patientExists) throw new NotFound("Patient not found");
            var diseases = await _dbContext.DiseasePatients.Include(e => e.IddiseaseNavigation).Where(e => e.Idpatient == patientId).Select(e => new GetPatientDiseasesResponse()
            {
                IdDisease = e.Iddisease,
                IdPatientDisease = e.IddiseasePatient,
                Name = e.IddiseaseNavigation.Name,
                Description = e.IddiseaseNavigation.Description,
                Recommendation = e.IddiseaseNavigation.Recomendation,
                DateOfDiagnosis = e.Dateofdiagnosis,
                DateOfCure = e.Dateofcure
            }).ToListAsync();

            return diseases;
        }
        public async Task<List<SearchDiseaseResponse>> SearchDisease(string diseaseName)
        {
            if (string.IsNullOrWhiteSpace(diseaseName))
            {
                throw new InvalidData("Incorrect parameter diseaseName");
            }
            var disease = await _dbContext.Diseases.Where(e => e.Name.ToLower() == diseaseName.ToLower().Trim()).Select(e => new SearchDiseaseResponse()
            {
                IdDisease = e.Iddisease,
                Name = e.Name,
                Description = e.Description,
                Recommendation = e.Recomendation
            }).ToListAsync();
            if (disease.Count == 0) throw new NotFound("Disease not found");
            return disease;
        }

        public async Task<UpdateDiseaseResponse> UpdateDisease(UpdateDiseaseRequest request)
        {
            var disease = await _dbContext.Diseases.FirstOrDefaultAsync(e => e.Iddisease == request.IdDisease);
            if (disease == null) throw new NotFound("Disease not found");

            disease.Name = string.IsNullOrWhiteSpace(request.Name) ? disease.Name : request.Name;

            disease.Recomendation = string.IsNullOrWhiteSpace(request.Recomendation) ? disease.Recomendation : request.Recomendation;

            disease.Description = string.IsNullOrWhiteSpace(request.Description) ? disease.Description : request.Description;

            await _dbContext.SaveChangesAsync();
            return new UpdateDiseaseResponse
            {
                Name = disease.Name,
                Description = disease.Description,
                Recomendation = disease.Recomendation
            };
        }

        public async Task UpdatePatientDisease(UpdatePatientDiseaseRequest request)
        {
            var diseaseAssign = await _dbContext.DiseasePatients.FirstOrDefaultAsync(e => e.IddiseasePatient == request.IdPatientDisease);
            if (diseaseAssign == null) throw new NotFound("Assign not found");
            diseaseAssign.Dateofcure = request.DateOfCure == null ? diseaseAssign.Dateofcure : request.DateOfCure;
            diseaseAssign.Dateofdiagnosis = request.DateOfDiagnosis == null ? diseaseAssign.Dateofdiagnosis : request.DateOfDiagnosis;
            await _dbContext.SaveChangesAsync();
        }
    }
}