using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface IReviewRepsoitory : IBaseRepository<Review>
    {
        Task<List<Review>> GetReviewsByCustomerIdAsync(int id);
        Task<List<Review>> GetAllReviewsAsync();
        Task<List<Review>> GetAllUnseenReviewsAsync();
        Task<Review> GetReviewById(int id);
    }
}
