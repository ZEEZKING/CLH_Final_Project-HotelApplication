using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Dtos.ResponseModel;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface IUserServices
    {
        Task<UserResponseModel> Login(LoginRequestModel model);
        Task<UsersResponseModel> GetUsersByRoleAsync(string role);
        Task<UserResponseModel> GetUserByTokenAsync(string token);
    }
}
