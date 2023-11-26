using CLH_Final_Project.Dtos.RequestModel;
using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleServices;

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromForm] CreateRoleRequestModel model)
        {
            var role = await _roleServices.CreateRole(model);
            if(role.Sucesss == false) 
            {
                return BadRequest(role);
            }
            return Ok(role);
        }

        [HttpGet("GetRole/{id}")]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            var role = await _roleServices.GetRoleByUserId(id);
            if(role.Sucesss == false) 
            {
                return BadRequest(role);
            }
            return Ok(role);
        }

        [HttpGet("GetAllRolesAsync")]
        public async Task<IActionResult> GetAllRole()
        {
            var roles = await _roleServices.GetAllRoleAsync();
            if(roles.Sucesss == false)
            {
                return BadRequest(roles);
            }
            return Ok(roles);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromForm]UpdateRoleRequestModel model, [FromRoute] int id)
        {
            var role = await _roleServices.UpdateRoleAsync(model, id);
            if (role.Sucesss == false)
            {
                return BadRequest(role);
            }
            return Ok(role);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            var role = await _roleServices.DeleteRole(id);
            if (role.Sucesss == false)
            {
                return BadRequest(role);
            }
            return Ok(role);
        }

       
    }
}
