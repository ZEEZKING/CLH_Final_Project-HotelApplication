using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.Entities;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;

namespace CLH_Final_Project.Implementation.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepository _roleRepository;
        public RoleServices(IRoleRepository roleRepository) 
        {
            _roleRepository = roleRepository;
        }
        public async Task<BaseResponse> CreateRole(CreateRoleRequestModel model)
        {
            var role = await _roleRepository.GetAsync(x => x.Name == model.Name);
            if (role != null)
            {
                return new BaseResponse
                {
                    Message = "Role Already Exist",
                    Sucesss = false,
                };
            }
            var newRole = new Roles
            {
                Name = model.Name.ToLower(),
                Description = model.Description,
            };
            await _roleRepository.CreateAsync(newRole);
            return new BaseResponse
            {
               Message = $"You Have SucessFully Created A role {newRole.Name}",
               Sucesss = true,     
            };
        }

        public async Task<BaseResponse> DeleteRole(int id)
        {
            var role = await _roleRepository.GetRoleByUserId(id);
            if (role != null)
            {
                role.IsDeleted = true;
                await _roleRepository.UpdateAsync(role);
                return new BaseResponse
                {
                    Message = "Role is Deleted Sucessfully",
                    Sucesss = true
                };
            }
            return new BaseResponse
            {
                Message = "Role Deleted SucessFully",
                Sucesss = false,
            };
        }

        public async Task<RolesResponseModel> GetAllRoleAsync()
        {
            var role = await _roleRepository.GetAllRoleAsync();
            if (role == null)
            {
                return new RolesResponseModel
                {
                    Message = "role not found",
                    Sucesss = false,
                };
            }
            return new RolesResponseModel
            {
                Message = "Role sucessfully found",
                Sucesss = true,
                Data = role.Select(x => new RoleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList()
            };
        }

        public async Task<RoleResponseModel> GetRoleByUserId(int id)
        {
            var role = await _roleRepository.GetRoleByUserId(id);
            if (role == null)
            {
                return new RoleResponseModel
                {
                    Message = "Role not found",
                    Sucesss = false
                };
            }
            return new RoleResponseModel
            {
                Message = "Role Found",
                Sucesss = true,
                Data = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description
                }

            };
        }




        public async Task<RoleResponseModel> UpdateRoleAsync(UpdateRoleRequestModel model, int id)
        {
            var role = await _roleRepository.GetAsync(id);
            if(role == null)
            {
                return new RoleResponseModel
                {
                    Message = "Role Not Found",
                    Sucesss = false
                };
            }
            role.Name = model.Name ?? role.Name;
            role.Description = model.Description ?? role.Description;
            var roleUpt = await _roleRepository.UpdateAsync(role);
            return new RoleResponseModel
            {
                Message = "Role Successfully Updated",
                Sucesss = true,
                Data = new RoleDto 
                {
                    Id = roleUpt.Id,
                    Name = roleUpt.Name,
                    Description = roleUpt.Description

                }
            };
        }
    }
}
