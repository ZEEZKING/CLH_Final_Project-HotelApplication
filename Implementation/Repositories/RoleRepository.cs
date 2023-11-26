using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class RoleRepository : BaseRepository<Roles>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext Conext) 
        { 
            _Context = Conext;
        }

        public async Task<List<Roles>> GetAllRoleAsync()
        {
            return await _Context.Roles
            .Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Roles> GetRoleByName(string name)
        {
            return await _Context.Roles
                .Where(x => x.Name == name).SingleOrDefaultAsync();
        }

        public async Task<Roles> GetRoleByUserId(int id)
        {
            var role =  await _Context.Roles
                .Where(c => c.Id == id).FirstOrDefaultAsync();
            return role;
        }
    }
}
