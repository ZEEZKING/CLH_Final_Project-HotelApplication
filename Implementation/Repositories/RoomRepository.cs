using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext Context) 
        { 
            _Context = Context;
        }
        public async Task<List<Room>> GetAllAvailableRoomsAsync()
        {
            return await _Context.Rooms
             .Where(x => x.IsAvailable == false && x.IsDeleted == false)
             .ToListAsync();
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _Context.Rooms
            .Where(x => x.IsDeleted == false)
            .ToListAsync();


        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _Context.Rooms
            .Where(x => x.Id == id && x.IsDeleted == false).Include(x => x.Bookings)
            .SingleOrDefaultAsync();
        }

        public async Task<Room> GetRoomByNameAsync(string name)
        {
            return await _Context.Rooms
            .Where(x => x.RoomName == name)
            .SingleOrDefaultAsync();
        }

        public async Task<List<Room>> GetUnAvailableRoomsAsync()
        {
           return await _Context.Rooms
            .Where(x => x.IsAvailable == true && x.IsDeleted == false)
            .ToListAsync();
        }
    }
}
