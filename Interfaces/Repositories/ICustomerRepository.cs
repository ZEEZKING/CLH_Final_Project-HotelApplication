using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer> 
    {
        Task<Customer> GetCustomerByUserIdAsync(int id);
        Task<Customer> GetByEmailAsync(string email);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<List<Customer>> GetAllCustomers();

    }
}
