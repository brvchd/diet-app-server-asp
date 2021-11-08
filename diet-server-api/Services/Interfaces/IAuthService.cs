using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Auth;
using diet_server_api.DTO.Responses.Auth;

namespace diet_server_api.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponse> Login(LoginRequest request);
        public Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request);
    }
}