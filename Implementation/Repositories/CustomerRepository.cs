using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Diagnostics.Metrics;
using System.IO;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext Context) 
        { 
            _Context = Context;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
                return await _Context.Customers
                .Include(x => x.User).Where(x => x.User.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
              return await _Context.Customers
             .Include(c => c.User)
            .SingleOrDefaultAsync(x => x.User.Email == email);
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _Context.Customers
            .Where(c => c.Id == id && c.IsDeleted == false)
            .Include(c => c.User).SingleOrDefaultAsync();
        }

        public async Task<Customer> GetCustomerByUserIdAsync(int id)
        {
            return await _Context.Customers
             .Where(x => x.User.Id == id && x.IsDeleted == false)
             .Include(x => x.User).SingleOrDefaultAsync();
                
        }
    }
}
