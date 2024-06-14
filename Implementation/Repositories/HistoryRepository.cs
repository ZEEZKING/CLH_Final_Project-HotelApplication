using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class HistoryRepository : BaseRepository<History>, IHistoryRepository
    {

        public HistoryRepository(ApplicationDbContext context) 
        {
            _Context = context;
        }
        public async Task<List<History>> GetAllHistorys()
        {
           return await _Context.History
                .Include(x => x.Bookings).
                Where(x => x.IsDeleted == false)
                .ToListAsync();
        }

        public async Task<History> GetHistoryByCustomerId(int customerId)
        {
            return await _Context.History
                .Include(x => x.Customer)
                .Where(c => c.CustomerId == customerId)
                .Include(x => x.Bookings)
                .SingleOrDefaultAsync();
        }

        public async Task<History> GetHistoryById(int id)
        {
            return await _Context.History
                .Include(x => x.Bookings)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}
