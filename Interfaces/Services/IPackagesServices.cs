using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface IPackagesServices
    {
        Task<PackageResponseModel> CreatePackages(CreatePackagesRequestModel model);
        Task<PackageResponseModel> GetPackageById(int id);
        Task<PackagesResponseModel> GetAllPackages();
        Task<PackageResponseModel> UpdatePackage(UpdatePackageRequestModel model, int id);
        Task<PackageResponseModel> DeletePackage(int id);
    }
}
