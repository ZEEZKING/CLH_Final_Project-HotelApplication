using CLH_Final_Project.Auth;
using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.EmailServices;
using CLH_Final_Project.Entities;
using CLH_Final_Project.Implementation.Repositories;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;



namespace CLH_Final_Project.Implementation.Services
{
    public class CustomerServices : ICustomerServices
    {
       // private string generatedToken = null;

        private readonly ICustomerRepository _customerRespository;
        private readonly IRoleRepository _roleRespository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserRepository _userRepository;
        private readonly IMailServices _mailServices;
        private readonly IJWTAuthenticationManager _manager;
        private readonly IConfiguration _config;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        public CustomerServices(ICustomerRepository customerRepository,IWebHostEnvironment webHostEnvironment,IUserRepository userRepository,IRoleRepository roleRepository,IMailServices mailServices,IJWTAuthenticationManager manager,IConfiguration config,IVerificationCodeRepository verificationCodeRepository)
        {
            _customerRespository = customerRepository;
            _webHostEnvironment = webHostEnvironment;
            _userRepository = userRepository;
            _roleRespository = roleRepository;
            _mailServices = mailServices;
            _manager = manager;
            _config = config;
            _verificationCodeRepository = verificationCodeRepository;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var customer = await _customerRespository.GetCustomerByUserIdAsync(id);
            if (customer == null)
            {
                return new CustomerResponseModel
                {
                    Message = "Customer Not Found",
                    Sucesss = false
                };

            }
            customer.User.IsDeleted = true;
            customer.IsDeleted = true;
            await _customerRespository.UpdateAsync(customer);
            await _userRepository.UpdateAsync(customer.User);
            return new BaseResponse
            {
                Message = "Custoemr Deleted Successfully",
                Sucesss = true
            };
        }

        public async Task<CustomersResponseModel> GetAllCustomersAsync()
        {
            var customers = await _customerRespository.GetAllCustomers();
            
            if (customers !=  null)
            {
                foreach (var item in customers)
                {
                    if(item.IsDeleted == false)
                    {
                        return new CustomersResponseModel
                        {
                            Message = "Customers Found Successfully",
                            Sucesss = true,
                            Data = customers.Select(x => new CustomerDto
                            {
                                Id = x.User.Id,
                                Name = x.User.Name,
                                UserName = x.User.UserName,
                                Email = x.User.Email,
                                PhoneNumber = x.User.PhoneNumber,
                                Address = x.User.Address,
                                Image = x.User.ProfileImage,

                            }).ToList()

                        };
                    }
                }

            }
           
            return new CustomersResponseModel
            {
                Message = "Customers Not Found",
                Sucesss = false
            };
        }

        public async Task<CustomerResponseModel> GetByIdAsync(int id)
        {
            var customer = await _customerRespository.GetCustomerByIdAsync(id);
            //var customer = await _customerRespository.GetCustomerByUserIdAsync(id);
            if (customer == null)
            {
                return new CustomerResponseModel
                {
                    Message = "Customer Not Found",
                    Sucesss = false
                };
                
            }
            return new CustomerResponseModel
            {
                Message = "Customer Record found",
                Sucesss = true,
                Data = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.User.Name,
                    UserName = customer.User.UserName,
                    PhoneNumber =customer.User.PhoneNumber,
                    Email = customer.User.Email,
                    Image =customer.User.ProfileImage,
                    Address = customer.User.Address,
                }
            };

        }

        public async Task<CustomerResponseModel> Register(CreateCustomerRequestModel model)
        {
            int random = new Random().Next(10000, 99999);
            var exist = await _customerRespository.ExistsAsync(x => x.User.Email == model.Email && x.User.IsDeleted == false);
            if (exist)
            {
                return new CustomerResponseModel
                {
                    Message = "Email already in Use",
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
                using (var fileStream = new FileStream(fullPath,FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            var user = new User
            {
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                Age = Age(model.Age),
                ProfileImage = imageName,
                Address = model.Address,
                Gender = model.Gender,
                DateCreated = DateTime.Now,

            };
            var role = await _roleRespository.GetAsync(x => x.Name == "Customer");
            if (role == null)
            {
                return new CustomerResponseModel
                {
                    Message = "Role Not Found",
                    Sucesss = false
                };
            }
            var  addUser = await _userRepository.CreateAsync(user);
            var userRole = new UserRoles
            {
                UserId = user.Id,
                RoleId = role.Id
            };
            addUser.UserRoles.Add(userRole);
            var customer = new Customer
            {
                UserId = user.Id,
                User = addUser,

            };
            var cust = await _customerRespository.CreateAsync(customer);

            var code = new VerificationCode
            {
                Code = random,
                CustomerId = cust.Id
            };
            await _verificationCodeRepository.CreateAsync(code);

            var mailRequest = new MailRequset
            {
                Subject = "Welcome",
                ToEmail = user.Email,
                ToName = user.Name,
                HtmlContent = $"<html><body><h1>Hello {customer.User.Name}, Welcome to SkyBox Hotel Limited.</h1><h4>Your confirmation code is {code.Code} to continue with the registration <a href=https://www.canva.com/design/DAFz_ES31XE/EtoIii982LTYgm6K-VYZgg/edit?utm_content=DAFz_ES31XE&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton></h4></body></html>"

            };
             _mailServices.SendEmailAsync(mailRequest);

            return new CustomerResponseModel
            {
                Message = "Sucessfully Registered",
                Sucesss = true,
                Data = new CustomerDto
                { 
                    Id = customer.Id,
                    Name = customer.User.Name,
                    Email = customer.User.Email,
                    UserName = customer.User.UserName,
                    PhoneNumber = customer.User.PhoneNumber,
                    Address = customer.User.Address,
                    Image = customer.User.ProfileImage
                }
            };
        }

        public async Task<CustomerResponseModel> UpdateProfile(UpdateCustomerRequestModel model, int id)
        {
            var customer = await _customerRespository.GetCustomerByUserIdAsync(id);
            if (customer == null)
            {
                return new CustomerResponseModel
                {
                    Message = "Customer Not Found",
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
            }
            customer.User.Name = model.Name ?? customer.User.Name;
            customer.User.UserName = model.UserName ?? customer.User.UserName;
            customer.User.Email = model.Email ?? customer.User.Email;
            customer.User.PhoneNumber = model.PhoneNumber ?? customer.User.PhoneNumber;
            customer.User.ProfileImage = imageName;

            var custUpt = await _customerRespository.UpdateAsync(customer);
            return new CustomerResponseModel
            {
                Message = "Profile Updated Sucessfully",
                Sucesss = true,
                Data = new CustomerDto
                {
                    Id = custUpt.Id,
                    Name = custUpt.User.Name,
                    UserName = custUpt.User.UserName,
                    Email = custUpt.User.Email,
                    PhoneNumber = custUpt.User.PhoneNumber,
                    Image = custUpt.User.ProfileImage,
                }
            };
        }

        private int Age(int age)
        {
            if (age >= 18)
            {
                return age;

            }
            else
            {
                Console.WriteLine("Age Should be Greater than 18");
                return 0;
            }
        }
    }
}
