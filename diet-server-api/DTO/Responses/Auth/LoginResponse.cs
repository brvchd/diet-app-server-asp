namespace diet_server_api.DTO.Responses.Auth
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}