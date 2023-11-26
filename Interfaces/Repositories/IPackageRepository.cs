using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface IPackageRepository : IBaseRepository<Packages>
    {
        Task<Packages> GetPackagesByIdAsync(int id);
        Task<List<Packages>> GetAllPackagesAsync(); 
    }
}
