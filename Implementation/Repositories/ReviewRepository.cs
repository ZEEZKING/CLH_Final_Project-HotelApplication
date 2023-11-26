using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepsoitory
    {
        public ReviewRepository(ApplicationDbContext Context) 
        {
            _Context = Context;
        }
        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _Context.Reviews
            .Include(c => c.Customer)
            .ThenInclude(u => u.User)
            .Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<Review>> GetAllUnseenReviewsAsync()
        {
            return await _Context.Reviews
            .Include(c => c.Customer)
            .ThenInclude(u => u.User)
            .Where(x => x.Seen == false && x.IsDeleted == false)
            .ToListAsync();
        }

        public async Task<Review> GetReviewById(int id)
        {
           return await _Context.Reviews
            .Include(c => c.Customer)
            .ThenInclude(u => u.User)
            .Where(x => x.Id == id)
            .SingleAsync();
        }

        public async Task<List<Review>> GetReviewsByCustomerIdAsync(int id)
        {
            return await _Context.Reviews
             .Where(x => x.CustomerId == id)
             .Include(c => c.Customer)
             .ThenInclude(u => u.User)
             .Where(x => x.IsDeleted == false)
             .ToListAsync();

        }
    }
}
