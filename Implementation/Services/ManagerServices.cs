using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.EmailServices;
using CLH_Final_Project.Entities;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;

namespace CLH_Final_Project.Implementation.Services
{
    public class ManagerServices : IManagerServices
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMailServices _mailServices;

        public ManagerServices(IManagerRepository managerRepository,IRoleRepository roleRepository,IWebHostEnvironment webHostEnvironment,IUserRepository userRepository,IMailServices mailServices)
        { 
            _managerRepository = managerRepository;
            _roleRepository = roleRepository;
            _webHostEnvironment = webHostEnvironment;
            _userRepository = userRepository;
            _mailServices = mailServices;
        }

        public async Task<BaseResponse> AddManager(CreateManagerRequestModel model)
        {
            var users = await _userRepository.GetAsync(u => u.Email == model.Email);
            if (users != null)
            {
                return new BaseResponse
                {
                    Message = "User Already Exist",
                    Sucesss = false,
                };
            }
            var manager  = await _managerRepository.GetManagerByRoleAsync(model.Role);
            if(manager != null)
            {
                return new BaseResponse
                {
                    Message = $"A new Manager cant be add with this role because the former {model.Role} is still in use",
                    Sucesss = false,
                };
            }
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                IsDeleted = false,
            };
            
            var userDto = new UserDto
            {
                Id = user.Id,
                Email = model.Email,
                Role = model.Role,
            };

            var role = await _roleRepository.GetAsync(x => x.Name == model.Role);
            if (role == null)
            {
                return new BaseResponse
                {
                    Message = "Role not found",
                    Sucesss = false,
                };
            }
            var userRole = new UserRoles
            {
                UserId = user.Id,
                RoleId = role.Id,
            };
            var addUser = await _userRepository.CreateAsync(user);

            addUser.UserRoles.Add(userRole);
            await _userRepository.UpdateAsync(addUser);

            var manage = new Manager
            {
                UserId = addUser.Id,
                User = addUser,
                IsDeleted = false,
            };

            var addManager = await _managerRepository.CreateAsync(manage);

            var mailRequest = new MailRequset
            {
                Subject = "Complete Your registration",
                ToEmail = user.Email,
                ToName = model.Name,
               // HtmlContent = $"<!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\"><title>Email Confirmation</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><style type=\"text/css\">@media screen {{@font-face {{font-family: 'Source Sans Pro';font-style: normal;font-weight: 400;src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');}}@font-face {{font-family: 'Source Sans Pro';font-style: normal;font-weight: 700;src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');}}body,table,td,a {{-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;}}table,td {{mso-table-rspace: 0pt;mso-table-lspace: 0pt;}}img {{-ms-interpolation-mode: bicubic;}}a[x-apple-data-detectors] {{font-family: inherit !important;font-size: inherit !important;font-weight: inherit !important;line-height: inherit !important;color: inherit !important;text-decoration: none !important;}}div[style*=\"margin: 16px 0;\"] {{margin: 0 !important;}}body {{width: 100% !important;height: 100% !important;padding: 0 !important;margin: 0 !important;}}table {{border-collapse: collapse !important;}}a {{color: #1a82e2;}}img {{height: auto;line-height: 100%;text-decoration: none;border: 0;outline: none;}}</style></head><body style=\"background-color: #f5f5dc;\"><div class=\"preheader\" style=\"display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;\">A preheader is the short summary text that follows the subject line when an email is viewed in the inbox.</div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 36px 24px;\"><a href=\"https://sendgrid.com\" target=\"_blank\" style=\"display: inline-block;\"><img src=\"https://media.licdn.com/dms/image/C510BAQHtR8AdDc-aJg/company-logo_200_200/0/1519909536138?e=2147483647&v=beta&t=n-uF8UVHI5jdSuAZ61e6OVnV1n8PWocgp3lZ0igTpyg\" alt=\"Logo\" border=\"0\" width=\"100\" height=\"100\" style=\"display: block;border-radius: 50%;\"></a></td></tr></table></td></tr><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;\"><h2 style=\"margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;\">Confirm Your Email Address</h2></td></tr></table></td></tr><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\"><p style=\"margin: 0;\">Tap the button below to confirm your email address. If you didn't create an account with <strong>SkyBox</strong>, you can safely delete this email.</p></td></tr><tr><td align=\"left\" bgcolor=\"#ffffff\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#ffffff\" style=\"padding: 12px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td align=\"center\" bgcolor=\"#1a82e2\" style=\"border-radius: 6px;\"><a href=\"http://127.0.0.1:5501/AdminFrontEnd/completeRegistration.html?token={addUser.Token}\" target=\"_blank\" style=\"display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;\">Confirm</a></td></tr></table></td></tr></table></td></tr></td></tr></table><body></html>"
               HtmlContent = $"<!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\"><title>Email Confirmation</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"><style type=\"text/css\">@media screen {{@font-face {{font-family: 'Source Sans Pro';font-style: normal;font-weight: 400;src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');}}@font-face {{font-family: 'Source Sans Pro';font-style: normal;font-weight: 700;src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');}}body,table,td,a {{-ms-text-size-adjust: 100%;-webkit-text-size-adjust: 100%;}}table,td {{mso-table-rspace: 0pt;mso-table-lspace: 0pt;}}img {{-ms-interpolation-mode: bicubic;}}a[x-apple-data-detectors] {{font-family: inherit !important;font-size: inherit !important;font-weight: inherit !important;line-height: inherit !important;color: inherit !important;text-decoration: none !important;}}div[style*=\"margin: 16px 0;\"] {{margin: 0 !important;}}body {{width: 100% !important;height: 100% !important;padding: 0 !important;margin: 0 !important;}}table {{border-collapse: collapse !important;}}a {{color: #1a82e2;}}img {{height: auto;line-height: 100%;text-decoration: none;border: 0;outline: none;}}</style></head><body style=\"background-color: #e9ecef;\"><div class=\"preheader\" style=\"display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;\">A preheader is the short summary text that follows the subject line when an email is viewed in the inbox.</div><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 36px 24px;\"><a href=https://www.canva.com/design/DAFz_ES31XE/EtoIii982LTYgm6K-VYZgg/edit?utm_content=DAFz_ES31XE&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton\" target=\"_blank\" style=\"display: inline-block;\"><img src=\"https://www.canva.com/design/DAFz_ES31XE/EtoIii982LTYgm6K-VYZgg/edit?utm_content=DAFz_ES31XE&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton\" alt=\"Logo\" border=\"0\" width=\"100\" height=\"100\" style=\"display: block;border-radius: 50%;\"></a></td></tr></table></td></tr><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;\"><h2 style=\"margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;\">Confirm Your Email Address</h2></td></tr></table></td></tr><tr><td align=\"center\" bgcolor=\"#e9ecef\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"left\" bgcolor=\"#ffffff\" style=\"padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;\"><p style=\"margin: 0;\">Tap the button below to confirm your email address. If you didn't create an account with <strong>SkyBox</strong>, you can safely delete this email.</p></td></tr><tr><td align=\"left\" bgcolor=\"#ffffff\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td align=\"center\" bgcolor=\"#ffffff\" style=\"padding: 12px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td align=\"center\" bgcolor=\"#1a82e2\" style=\"border-radius: 6px;\"><a href=\"http://127.0.0.1:5501/AdminFrontEnd/completeReg.html\"target=\"_blank\" style=\"display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;\">Confirm</a></td></tr></table></td></tr></table></td></tr></td></tr></table><body></html>"

            };
            _mailServices.SendEmailAsync(mailRequest);

            return new BaseResponse
            {
                Message = "Admin added sucessfully",
                Sucesss = true,
            };



        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var managers = await _managerRepository.GetManagerById(id);
            if (managers == null)
            {
                return new BaseResponse
                {
                    Message = "Manager Not Found",
                    Sucesss = false,
                };
            }
            managers.IsDeleted = true;
            managers.User.IsDeleted = true;
           await _managerRepository.UpdateAsync(managers);
            await _userRepository.UpdateAsync(managers.User);
            return new BaseResponse
            {
                Message = "Manager Successfully Deleted ",
                Sucesss = true,
            };
        }

        public async Task<ManagersResponseModel> GetAllManagers()
        {
            var managers = await _managerRepository.GetAllManager();
            var role = await _roleRepository.GetAllAsync();
            if (managers == null)
            {
                return new ManagersResponseModel
                {
                    Message = "No Managers yet",
                    Sucesss = false
                };
            }
            return new ManagersResponseModel
            {
                Message = "managers Found",
                Sucesss = true,
                Data = managers.Select(x => new ManagerDto
                {
                    Id = x.Id,
                    Name = x.User.Name,
                    UserName = x.User.UserName,
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber,
                    Address = x.User.Address,
                    Image = x.User.ProfileImage,
                    Gender = x.User.Gender.ToString(),
                    RoleName = x.Role.Name
                }).ToList(),
            };
        }

        public async Task<ManagerResponseModel> GetManagerById(int id)
        {
            //var manageId = await _managerRepository.GetManagerById(id);
            var manageId = await _managerRepository.GetManagerByUserIdAsync(id);
         

            if (manageId == null)
            {
                return new ManagerResponseModel
                {
                    Message = "manager not found",
                    Sucesss = false,

                };
            }
            return new ManagerResponseModel
            {
                Message = "Manager Found Sucessfully",
                Sucesss = true,
                Data = new ManagerDto
                {
                    Id = manageId.Id,
                    Name = manageId.User.Name,
                    UserName = manageId.User.UserName,
                    Email = manageId.User.Email,
                    PhoneNumber = manageId.User.PhoneNumber,
                    Image = manageId.User.ProfileImage,
                    Address = manageId.User.Address,
                    Gender = manageId.User.Gender.ToString(),
                    
                }
            };
        }

        public async Task<BaseResponse> RegisterFully(CompleteManagerRegistration model)
        {
            var manage = await _managerRepository.GetManagerByEmailAsync(model.Email);
            if (manage == null)
            {
                return new BaseResponse
                {
                    Message = "Manager Not Found",
                    Sucesss = false,
                };
            }
            var imageName = "";
            if (model.ProfileImage != null)
            {
                var imgPath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(imgPath, "Images");
                Directory.CreateDirectory(imagePath);
                var imagetype = model.ProfileImage.ContentType.Split('/')[1];
                imageName = $"{Guid.NewGuid()}.{imagetype}";
                var fullPath = Path.Combine(imagePath, imageName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            manage.User.Name = model.Name ?? manage.User.Name;
            manage.User.UserName ??= model.UserName;
            manage.User.Email = model.Email ?? manage.User.Email;
            manage.User.PhoneNumber = model.PhoneNumber ?? manage.User.PhoneNumber;
            manage.User.ProfileImage = imageName;
            manage.User.Address = model.Address ?? manage.User.Address;
            manage.User.Age = model.Age;
            manage.User.IsDeleted = false;
            manage.User.Password = model.Password;
            manage.User.Gender = model.Gender;

            var manages = await _managerRepository.UpdateAsync(manage);

            return new BaseResponse
            {
                Message = "Registration Completed Sucessfully",
                Sucesss = true
            };


            

        }

        public async Task<ManagerResponseModel> UpdateProfile(UpdateManagerRequestModel model, int id)
        {
            var manage = await _managerRepository.GetManagerByUserIdAsync(id);
            if (manage == null)
            {
                return new ManagerResponseModel
                {
                    Message = "Managet Not Found",
                    Sucesss = false
                };
            }

            var imageName = "";
            if (model.ProfileImage != null)
            {
                var imgPath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(imgPath, "Images");
                Directory.CreateDirectory(imagePath);
                var imagetype = model.ProfileImage.ContentType.Split('/')[1];
                imageName = $"{Guid.NewGuid()}.{imagetype}";
                var fullPath = Path.Combine(imagePath, imageName);
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
                manage.User.ProfileImage = imageName;
            }

            else if (model.ProfileImage == null)
            {
                manage.User.ProfileImage = manage.User.ProfileImage;
            }

            manage.User.UserName = model.UserName ?? manage.User.UserName;
            manage.User.Name = model.Name ?? manage.User.Name;
            manage.User.Email = model.Email ?? manage.User.Email;
            manage.User.PhoneNumber = model.PhoneNumber ?? manage.User.PhoneNumber; 
            manage.User.Address = model.Address ?? manage.User.Address;

            var manages = await _managerRepository.UpdateAsync(manage);
            return new ManagerResponseModel
            {
                Message = "Profile Updated SucessFully",
                Sucesss = true,
                Data = new ManagerDto
                {
                    Id = manage.Id,
                    Name = manage.User.Name,
                    Email = manage.User.Email,
                    UserName = manage.User.UserName,
                    PhoneNumber = manage.User.PhoneNumber,
                    Address = manage.User.Address,
                    Image = manages.User.ProfileImage,
                    Gender = manages.User.Gender.ToString(),
                }
            };

        }
    }
}
