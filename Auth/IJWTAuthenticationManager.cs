using CLH_Final_Project.Dtos;

namespace CLH_Final_Project.Auth
{
    public interface IJWTAuthenticationManager
    {
        string GenerateToken(string key, string issuer, UserDto user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
