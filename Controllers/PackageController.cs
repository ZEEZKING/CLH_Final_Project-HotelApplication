using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackagesServices _packageServices;

        public PackageController(IPackagesServices packageServices)
        {
            _packageServices = packageServices;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreatePackages([FromForm] CreatePackagesRequestModel model)
        {
            var packages = await _packageServices.CreatePackages(model);
            if(packages.Sucesss == false)
            {
                return BadRequest(packages);
            }
            return Ok(packages);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var packages = await _packageServices.GetPackageById(id);
            if(packages.Sucesss == false)
            {
                return BadRequest(packages);
            }
            return Ok(packages);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdatePackages([FromForm]UpdatePackageRequestModel model,[FromRoute] int id)
        {
            var packages = await _packageServices.UpdatePackage(model, id);
            if(packages.Sucesss == false)
            {
                return BadRequest(packages);
            }
            return Ok(packages);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPackages()
        {
            var packages = await _packageServices.GetAllPackages();
            if(packages.Sucesss == false)
            {
                return BadRequest(packages);
            }
            return Ok(packages);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var packages = await _packageServices.DeletePackage(id);
            if(packages.Sucesss == false)
            {
                return BadRequest(packages);
            }
            return Ok(packages);
        }
    }
}
