using CLH_Final_Project.Auth;
using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;

namespace CLH_Final_Project.Implementation.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IJWTAuthenticationManager _tokenServices;
        private readonly ICustomerRepository _customerRepository;
        private string generateToken = null;

        public UserServices(IUserRepository userRepository,IConfiguration config,IJWTAuthenticationManager tokenService,ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _config = config;
            _tokenServices = tokenService;
            _customerRepository = customerRepository;
        }

        public async Task<UserResponseModel> GetUserByTokenAsync(string token)
        {
            var user = await _userRepository.GetAsync(x => x.Token == token);
            if (user == null)
            {
                return new UserResponseModel
                {
                    Message = "User Not found",
                    Sucesss = false
                };
            }
            return new UserResponseModel
            {
                Message = "User found Successfully",
                Sucesss = true,
                Data = new UserDto
                {
                    Email = user.Email,
                }
            };
        }

        public async Task<UsersResponseModel> GetUsersByRoleAsync(string role)
        {
            var users = await _userRepository.GetUserByRolesAsync(role.ToLower());
            if (users == null)
            {
                return new UsersResponseModel
                {
                    Message = $"User with {role} role not found",
                    Sucesss = false
                };
            }
            return new UsersResponseModel
            {
                Message = $"User with {role} Role found",
                Sucesss = true,
                Data = users.Select(x => new UserDto
                {
                    Role = x.Role.Name,
                    Image = x.User.ProfileImage,
                    Id = x.User.Id,
                    Name = x.User.Name,

                }).ToList()

            };
        }

        public async Task<UserResponseModel> Login(LoginRequestModel model)
        {
            var userRole = await _userRepository.LoginAsync(model.Email, model.Password);
            var cust = await _customerRepository.GetByEmailAsync(model.Email);
           
            if (userRole != null)
            {
                var userDto = new UserDto
                {
                    Email = userRole.User.Email,
                    Id = userRole.User.Id,
                    Role = userRole.Role.Name
                };
                if (cust != null)
                {
                    cust.User.Token = generateToken = _tokenServices.GenerateToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), userDto);
                    await _userRepository.SaveChangesAsync();
                }
                return new UserResponseModel
                {
                    Message = $"Successfully Logged in as {userRole.Role.Name}",
                    Sucesss = true,
                    Data = new UserDto
                    {
                        Id = userRole.UserId,
                        Name = userRole.User.Name,
                        Email = userRole.User.Email,
                        UserName = userRole.User.Name,
                        Image = userRole.User.ProfileImage,
                        Role = userRole.Role.Name,
                        Token = generateToken = _tokenServices.GenerateToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), userDto)

                    }
                };
            }
            return new UserResponseModel
            {
                Sucesss = false,
                Message = "Login Failed"
            };
        }
    }
}
