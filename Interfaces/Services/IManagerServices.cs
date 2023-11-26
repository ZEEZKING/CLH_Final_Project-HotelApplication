using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface IManagerServices
    {
        Task<BaseResponse> AddManager(CreateManagerRequestModel model);
        Task<BaseResponse> RegisterFully(CompleteManagerRegistration model);
        Task<ManagerResponseModel> GetManagerById(int id);
        Task<ManagersResponseModel> GetAllManagers();
        Task<ManagerResponseModel> UpdateProfile(UpdateManagerRequestModel model, int id);
        Task<BaseResponse> DeleteAsync(int id);

    }
}
