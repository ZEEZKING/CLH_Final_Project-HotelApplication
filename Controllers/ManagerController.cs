using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerServices _managerServices;

        public ManagerController(IManagerServices managerServices)
        {
            _managerServices = managerServices;
        }

        [HttpPost("RegisterManager")]
        public async Task<IActionResult> RegisterManager([FromForm]CreateManagerRequestModel model)
        {
            var manager = await _managerServices.AddManager(model);
            if(manager.Sucesss == false)
            {
                return BadRequest(manager);
            }
            return Ok(manager); 
        }

        [HttpPost("CompleteRegistration")]
        public async Task<IActionResult> CompleteRegistration([FromForm] CompleteManagerRegistration model)
        {
            var manager = await _managerServices.RegisterFully(model);
            if (manager.Sucesss == false)
            {
                return BadRequest(manager);
            }
            return Ok(manager);
        }

        [HttpGet("GetAllManager")]
        public async Task<IActionResult> GetAllManager()
        {
            var managers = await _managerServices.GetAllManagers();
            if(managers.Sucesss == false)
            {
                return BadRequest(managers);
            }
            return Ok(managers);
        }

        [HttpGet("GetManager/{id}")]

        public async Task<IActionResult> GetManager([FromRoute]int id)
        {
            var manager = await _managerServices.GetManagerById(id);
            if(manager.Sucesss == false)
            {
                return BadRequest(manager);
            }
            return Ok(manager);
        }

        [HttpPut("UpdateManager/{id}")]
        public async Task<IActionResult> UpdateManager([FromForm] UpdateManagerRequestModel model, [FromRoute]int id)
        {
            var manager = await _managerServices.UpdateProfile(model, id);
            if (manager.Sucesss == false)
            {
                return BadRequest(manager);
            }
            return Ok(manager);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var manager = await _managerServices.DeleteAsync(id);
            if (manager.Sucesss == false)
            {
                return BadRequest(manager);
            }
            return Ok(manager);
        }
    }
}
