using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Auth;
using diet_server_api.DTO.Responses.Auth;

namespace diet_server_api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request);
        Task Logout(int IdUser);
    }
}