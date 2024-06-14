using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class SalesRepository : BaseRepository<Sale>, ISalesRepository
    {
        public SalesRepository(ApplicationDbContext Context) 
        {
            _Context = Context;
        }
        public async Task<List<Sale>> GetAllSales()
        {
            return await _Context.Sale.
                Include(x => x.Order)
                .ThenInclude(x => x.Customer)
                .Include(x => x.Booking)
                .ToListAsync();
               
        }

        public async Task<List<Sale>> GetSalesByCustomerIdASync(int id)
        {
            return await _Context.Sale
             .Include(x => x.Order)
             .Where(x => x.Order.CustomerId == id)
             .ToListAsync();
        }

        public async Task<List<Sale>> GetThisMonthSales()
        {
           return await _Context.Sale
            .Include(x => x.Order)
            .ThenInclude(x => x.Customer)
            .Include(x => x.Booking)
            .Where(x => x.DateCreated.Year == DateTime.Now.Year && x.CreatedOn.Month == DateTime.Now.Month)
            .ToListAsync();
        }

        public async Task<List<Sale>> GetThisYearSales()
        {
            return await _Context.Sale
            .Include(x => x.Order)
            .ThenInclude(x => x.Customer)
            .Include(x => x.Booking)
            .Where(x => x.DateCreated.Year == DateTime.Now.Year)
            .ToListAsync();
        }

        public async Task<double> GetTotalMonthlySalesAsync()
        {
            return await _Context.Sale
            .Where(x => x.CreatedOn.Month == DateTime.Now.Month)
            .SumAsync(x => x.AmountPaid);
        }

        public async Task<double> GetTotalMonthlySalesAsync(int month, int year)
        {
            return await _Context.Sale
           .Where(x => x.CreatedOn.Month == month && x.CreatedOn.Year == year )
           .SumAsync(x => x.AmountPaid);
        }

        public  async Task<double> GetTotalYearlySalesAsync()
        {
            return await _Context.Sale
            .Where(x => x.CreatedOn.Year == DateTime.Now.Year)
            .SumAsync(x => x.AmountPaid);
        }

        public async Task<double> GetTotalYearlySalesAsync(int year)
        {
            return await _Context.Sale
          .Where(x => x.CreatedOn.Year == year)
          .SumAsync(x => x.AmountPaid);
        }
    }
}
