using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLH_Final_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        [HttpPost("RegisterCustomer")]

        public async Task<IActionResult> Register([FromForm] CreateCustomerRequestModel model)
        {
            var customer = await _customerServices.Register(model);
            if (customer.Sucesss == false)
            {
                return BadRequest(customer);
            }
            return Ok(customer);
        }

        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateAsync([FromForm] UpdateCustomerRequestModel model, [FromRoute]int id)
        {
            var cust = await _customerServices.UpdateProfile(model,id);
            if(!cust.Sucesss)
            {
                return BadRequest(cust);
            }
            return Ok(cust);
        }

        [HttpGet("Get/{id}")]

        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            var customer = await _customerServices.GetByIdAsync(id);
            if(customer.Sucesss == false)
            {
                return BadRequest(customer);
            }
            return Ok(customer);
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var customers = await _customerServices.GetAllCustomersAsync();
            if (customers.Sucesss == false)
            {
                return BadRequest(customers);
            }
            return Ok(customers);
        }

        [HttpDelete("Delete/int:{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            var customer = await _customerServices.DeleteAsync(id); 
            if (customer.Sucesss == false)
            {
                return BadRequest(customer);
            }
            return Ok(customer);
        }


    }
}
