using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<UserRoles> LoginAsync(string email, string password);
        Task<List<UserRoles>> GetUserByRolesAsync(string role);  
        Task<User> GetUserById(int id);

    }
}
