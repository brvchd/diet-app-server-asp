using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Auth;
using diet_server_api.DTO.Responses.Auth;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Helpers.Auth;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace diet_server_api.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly mdzcojxmContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthService(mdzcojxmContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == request.Email && e.Isactive == true);
            if (user == null) throw new NotFound("User not found");
            var passwordToCompare = PasswordGenerator.GeneratePassword(request.Password, user.Salt);
            if (passwordToCompare != user.Password) throw new InvalidData("Incorrect credentials");
            if (user.Role == Roles.PATIENT)
            {
                var patient = await _dbContext.Patients.FirstOrDefaultAsync(e => e.Ispending == true && e.Iduser == user.Iduser);
                if (patient != null) throw new UserIsPending();
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = TokenGenerator.GenerateToken(user.Iduser, user.Role, user.Firstname, user.Lastname, creds);
            var refreshToken = RefreshTokenGenerator.GenerateRefreshToken();

            user.Refreshtoken = refreshToken;
            user.Refreshtokenexp = DateTime.Now.AddDays(2);

            await _dbContext.SaveChangesAsync();

            return new LoginResponse()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = refreshToken
            };
        }

        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Refreshtoken == request.RefreshToken);
            if (user == null) throw new RefreshTokenNotFound();
            if (user.Refreshtokenexp < DateTime.Now) throw new RefreshTokenExpired();


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = TokenGenerator.GenerateToken(user.Iduser, user.Role, user.Firstname, user.Lastname, creds);

            await _dbContext.SaveChangesAsync();

            return new RefreshTokenResponse()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = user.Refreshtoken
            };
        }
    }
}