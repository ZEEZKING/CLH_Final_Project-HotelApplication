using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface IManagerRepository : IBaseRepository<Manager>
    {
        Task<Manager> GetManagerById(int id);
        Task<Manager> GetManagerByUserIdAsync(int id);
        Task<List<UserRoles>> GetAllManager();
        Task<UserRoles> GetManagerByRoleAsync(string role);
        Task<Manager> GetManagerByEmailAsync(string email);
    }
}
