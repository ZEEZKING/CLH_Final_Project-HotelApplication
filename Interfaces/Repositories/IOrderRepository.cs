using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetOrderByCustomerId(int id);
        Task<List<Order>> GetAllOrdersAsync();
    }
}
