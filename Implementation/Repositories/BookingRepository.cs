using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext Context) 
        {
            _Context = Context; 
        }
        public async Task<List<Booking>> GetAllBookingAsync()
        {
           return await _Context.Bookings.ToListAsync();
        }

        public async Task<Booking> GetBookingByCard(string referenceCard)
        {
            return await _Context.Bookings
                .Where(x => x.ReferenceNo == referenceCard).SingleOrDefaultAsync();
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _Context.Bookings
            .Where(x => x.Id == id)
            .Include(x => x.Room)
            .FirstOrDefaultAsync();

        }

        public async Task UpdateCheckOutDateAsync(int id)
        {
            await _Context.SaveChangesAsync();
        }
    }
}
