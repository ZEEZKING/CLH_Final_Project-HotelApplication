using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface ICustomerServices
    {
        Task<BaseResponse> DeleteAsync(int id);
        Task<CustomerResponseModel> GetByIdAsync(int id);
        Task<CustomerResponseModel> Register(CreateCustomerRequestModel model);
        Task<CustomerResponseModel> UpdateProfile(UpdateCustomerRequestModel model, int id);
        Task<CustomersResponseModel> GetAllCustomersAsync();
    }
}
