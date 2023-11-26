using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface IRoleServices
    {
        Task<BaseResponse> CreateRole(CreateRoleRequestModel model);
        Task<RolesResponseModel> GetAllRoleAsync();
        Task<RoleResponseModel> UpdateRoleAsync(UpdateRoleRequestModel model, int id);
        Task<RoleResponseModel> GetRoleByUserId(int id);
        Task<BaseResponse> DeleteRole(int id);
    }
}
