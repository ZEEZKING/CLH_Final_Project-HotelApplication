using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface ISalesRepository : IBaseRepository<Sale>
    {
        Task<List<Sale>> GetAllSales();
        Task<List<Sale>> GetSalesByCustomerIdASync(int id);
        Task<List<Sale>> GetThisMonthSales();
        Task<List<Sale>> GetThisYearSales();
        Task<double> GetTotalMonthlySalesAsync();
        Task<double> GetTotalMonthlySalesAsync(int month,int year); 
        Task<double> GetTotalYearlySalesAsync();
        Task<double> GetTotalYearlySalesAsync(int  year);
    }
}
