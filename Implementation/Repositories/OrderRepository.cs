using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext Context) 
        { 
            _Context = Context;
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _Context.Orders.ToListAsync();
        }

        public async Task<List<Order>> GetOrderByCustomerId(int id)
        {
            return await _Context.Orders
            .Include(x => x.Customer)
            .Where(x => x.CustomerId == id)
            .ToListAsync(); 
        }
    }
}
