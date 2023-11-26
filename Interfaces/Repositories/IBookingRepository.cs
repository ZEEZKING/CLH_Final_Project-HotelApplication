using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<Booking> GetBookingByIdAsync(int id);
        Task<List<Booking>> GetAllBookingAsync();
        Task<Booking> GetBookingByCard(string referenceCard);
        Task UpdateCheckOutDateAsync(int id);
    }
}
