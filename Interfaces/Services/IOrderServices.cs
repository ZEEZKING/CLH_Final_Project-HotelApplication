using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface IOrderServices
    {
        Task<BaseResponse> CreateOrderAsync( int userId,int id);
        Task<OrderResponseModel> GetOrderByIdAsync(int id);
        Task<OrdersResponseModel> GetAllOrders();
    }
}
