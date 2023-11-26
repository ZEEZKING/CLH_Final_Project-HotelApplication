using CLH_Final_Project.Auth;
using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IConfiguration _config;
        private readonly IJWTAuthenticationManager _tokenServices;

        public UserController(IUserServices userServices, IConfiguration config, IJWTAuthenticationManager tokenServices)
        {
            _userServices = userServices;
            _config = config;
            _tokenServices = tokenServices;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestModel model)
        {
            var login = await _userServices.Login(model);
            if (login.Sucesss == false)
            {
                return BadRequest(login);
            }
            return Ok(login);
        }

        [HttpGet("GetUsersByRole/{role}")]

        public async Task<IActionResult> GetUsersByRoleAsync([FromRoute] string role)
        {
            var users = await _userServices.GetUsersByRoleAsync(role);
            if (users.Sucesss == false)
            {
                return BadRequest(users);
            }
            return Ok(users);
        }

        [HttpGet("GetUserByToken")]

        public async Task<IActionResult> GetUserByTokenAsync([FromQuery] string token)
        {
            var user = await _userServices.GetUserByTokenAsync(token);
            if (user.Sucesss == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }
    }
}
