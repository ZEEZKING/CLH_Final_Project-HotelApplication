using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(ApplicationDbContext Context) 
        {
            _Context = Context;
        }
        public async Task<List<UserRoles>> GetAllManager()
        {
            return await _Context.UserRoles
            .Include(x => x.User).Where(x => x.User.IsDeleted == false && x.Role.Name != "customer")
            .ToListAsync();
        }

        public async Task<Manager> GetManagerByEmailAsync(string email)
        {
            return await _Context.Managers
            .Include(x => x.User)
            .Where(x => x.User.Email == email)
            .SingleOrDefaultAsync();
        }

        public async Task<Manager> GetManagerById(int id)
        {
            return await _Context
                 .Managers.
                 Where(x => x.Id == id && x.IsDeleted == false).
                 Include(x => x.User).
                 SingleOrDefaultAsync();
                 
                
        }


        public async Task<UserRoles> GetManagerByRoleAsync(string role)
        {
            return await _Context.UserRoles
            .Include(x => x.Role)
            .Include(x => x.User)
            .Where(x => x.Role.Name == role && x.Role.IsDeleted == false)
            .SingleOrDefaultAsync();
        }

        public async Task<Manager> GetManagerByUserIdAsync(int id)
        {
            return await _Context.Managers
             .Where(x => x.User.Id == id && x.IsDeleted == false)
             .Include(x => x.User).SingleOrDefaultAsync();

        }
    }
}
