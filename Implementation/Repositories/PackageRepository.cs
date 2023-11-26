using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class PackageRepository : BaseRepository<Packages>, IPackageRepository
    {
        public PackageRepository(ApplicationDbContext Context)
        {
            _Context = Context;

        }
        public async Task<List<Packages>> GetAllPackagesAsync()
        {
            return await _Context.Packages.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Packages> GetPackagesByIdAsync(int id)
        {
            return await _Context.Packages
            .Where(x => x.Id == id && x.IsDeleted == false).SingleOrDefaultAsync();
        }
    }
}
