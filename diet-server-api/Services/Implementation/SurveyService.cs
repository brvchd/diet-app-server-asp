using System;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.DTO.Responses;
using diet_server_api.DTO.Responses.Survey;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation
{
    public class SurveyService : ISurveyService
    {
        private readonly mdzcojxmContext _dbContext;
        public SurveyService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SurveyAccessResponse> ValidateSurveyCredentials(SurveyCheckCredentialsRequest request)
        {
            var existingUser = await _dbContext.TempUsers.AnyAsync(e => e.Email == request.Email && e.Uniquekey == request.UniqueKey);
            if (!existingUser) throw new UserNotFound();
            return new SurveyAccessResponse() { Email = request.Email };
        }
    }
}