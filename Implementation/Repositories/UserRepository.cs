using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext Context) 
        {
            _Context = Context;
        }


        public async Task<User> GetUserById(int id)
        {
            return await _Context.Users
             .Where(x => x.Id == id)
             .Include(x => x.Customer)
             .Include(x => x.Manager)
             .SingleOrDefaultAsync();
        }

        public async Task<List<UserRoles>> GetUserByRolesAsync(string role)
        {
            return await _Context.UserRoles
            .Where(x => x.Role.Name == role && x.Role.IsDeleted == false)
            .Include(x => x.User)
            .Include(x => x.Role)
            .ToListAsync();
        }

        public async Task<UserRoles> LoginAsync(string email, string password)
        {
            return await _Context.UserRoles
            .Where(c => c.User.Email == email && c.User.Password == password && c.User.IsDeleted == false)
            .Include(c => c.User)
            .Include(c => c.Role)
            .SingleOrDefaultAsync();
        }
    }
}
