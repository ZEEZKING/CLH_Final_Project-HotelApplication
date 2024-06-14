using CLH_Final_Project.Dtos.ResponseModel;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface ISalesServices
    {
     
        Task<SalesResponseModel> CalculateAllMonthlySalesAsync(int year);
        Task<BaseResponse> CreateSales(int id,int bookingId);
        Task<SalesResponseModel> GetAllSales();
        Task<SalesResponseModel> GetSalesByCustomerNameAsync(string name);
        Task<SalesResponseModel> GetSalesByProductNameForTheMonth(int productId, int month, int year);

        /*Task<OrdersResponseModel> GetSalesForTheMonthOnEachProduct(int month, int year);
        Task<OrdersResponseModel> GetSalesForTheYearOnEachProduct(int year);*/
        Task<SalesResponseModel> GetSalesForThisMonth();
        Task<SalesResponseModel> GetSalesForThisYear();
    }
}
