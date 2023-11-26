using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.Entities;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;

namespace CLH_Final_Project.Implementation.Services
{
    public class PackagesServices : IPackagesServices
    {
        private readonly IPackageRepository _packages;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PackagesServices(IPackageRepository packages,IWebHostEnvironment webHostEnvironment)
        {
            _packages = packages;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<PackageResponseModel> CreatePackages(CreatePackagesRequestModel model)
        {
            var pack = await _packages.ExistsAsync(x => x.Name == model.Name);
            if(pack)
            {
                return new PackageResponseModel
                {
                    Message = "Package Name Already Exist",
                    Sucesss = false
                };
            }
            var imageName = "";
            if (model.PackageImage != null)
            {
                var imgPath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(imgPath, "Images");
                Directory.CreateDirectory(imagePath);
                var imagetype = model.PackageImage.ContentType.Split('/')[1];
                imageName = $"{Guid.NewGuid()}.{imagetype}";
                var fullPath = Path.Combine(imagePath, imageName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.PackageImage.CopyTo(fileStream);
                }
            }
            var packs = new Packages
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Types = model.Types,
                Images = imageName,

            };
            await _packages.CreateAsync(packs);
            return new PackageResponseModel
            {
                Message = "Yo  you have Successfully created a package",
                Sucesss = true,
                Data = new PackagesDto
                {
                    Id = packs.Id,
                    Name = packs.Name,
                    Description = packs.Description,
                    Price = packs.Price,
                    Types = packs.Types,
                    Images = packs.Images,
                },
            };

        }

        public async Task<PackageResponseModel> DeletePackage(int id)
        {
            var package = await _packages.GetPackagesByIdAsync(id);
            if (package == null)
            {
                return new PackageResponseModel
                {
                    Message = "Package not deleted",
                    Sucesss = false,

                };
            }
            package.IsDeleted = true;
            await _packages.UpdateAsync(package);
            return new PackageResponseModel
            {
                Message = "Package Already Deleted",
                Sucesss = true,
            };
        }

        public async Task<PackagesResponseModel> GetAllPackages()
        {
            var packages = await _packages.GetAllPackagesAsync();
            if (packages != null)
            {
                return new PackagesResponseModel
                {
                    Message = "Packages Found SuccessFully",
                    Sucesss = true,
                    Data = packages.Select(x => new PackagesDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Types = x.Types,
                        Images = x.Images,


                    }).ToList(),
                };
            }
            return new PackagesResponseModel
            {
                Message = "Packages Not Found",
                Sucesss = false
            };
        }

        public async Task<PackageResponseModel> GetPackageById(int id)
        {
            var packageId = await _packages.GetPackagesByIdAsync(id);
            if (packageId == null)
            {
                return new PackageResponseModel
                {
                    Message = "Package Not found",
                    Sucesss = false,
                };
            }
            return new PackageResponseModel
            {
                Message = "Packages Found",
                Sucesss = true,
                Data = new PackagesDto
                {
                    Id = packageId.Id,
                    Name = packageId.Name,
                    Description = packageId.Description,
                    Price = packageId.Price,
                    Images = packageId.Images,
                    Types = packageId.Types
                }
            };
        }

        public async Task<PackageResponseModel> UpdatePackage(UpdatePackageRequestModel model, int id)
        {
            var package = await _packages.GetPackagesByIdAsync(id);
            if (package == null)
            {
                return new PackageResponseModel
                {
                    Message = "package not found",
                    Sucesss = false,
                };
            }
            var imageName = "";
            if (model.PackageImage != null)
            {
                var imgPath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(imgPath, "Images");
                Directory.CreateDirectory(imagePath);
                var imagetype = model.PackageImage.ContentType.Split('/')[1];
                imageName = $"{Guid.NewGuid()}.{imagetype}";
                var fullPath = Path.Combine(imagePath, imageName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.PackageImage.CopyTo(fileStream);
                }
            }
            package.Name = model.Name ?? package.Name;
            package.Price = model.Price;
            package.Description = model.Description ?? package.Description;
            package.Types = model.Types ?? package.Types;
            package.Images = imageName;

            var packagesUpt = await _packages.UpdateAsync(package);
            return new PackageResponseModel
            {
                Message = "Packages Updated Successfully",
                Sucesss = true,
                Data = new PackagesDto
                {
                    Id = package.Id,
                    Name = packagesUpt.Name,
                    Price = packagesUpt.Price,
                    Description = packagesUpt.Description,
                    Images = packagesUpt.Images,
                    Types = packagesUpt.Types,
                }

            };

        }
    }
}
