using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        Task<Room> GetRoomByIdAsync(int id);
        Task<List<Room>> GetAllAvailableRoomsAsync();
        Task<List<Room>> GetUnAvailableRoomsAsync();
        Task<List<Room>> GetAllRooms();
        Task<Room> GetRoomByNameAsync(string name);
    }
}
