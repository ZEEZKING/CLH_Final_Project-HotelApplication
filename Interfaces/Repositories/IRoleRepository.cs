using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface IRoleRepository : IBaseRepository<Roles>
    {
        Task<Roles> GetRoleByUserId(int id);
        Task<Roles> GetRoleByName(string name);
        Task<List<Roles>> GetAllRoleAsync();

    }
}
