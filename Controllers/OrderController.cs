using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpPost("Create/{userId}/{packageId}")]
        public async Task<IActionResult> CreateOrderAsync([FromRoute]int userId,[FromRoute]int packageId)
        {
            var order = await _orderServices.CreateOrderAsync(userId, packageId);
            if(order.Sucesss == false)
            {
                return BadRequest(order);
            }
            return Ok(order);
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var orders = await _orderServices.GetAllOrders();
            if(orders.Sucesss == false)
            {
                return BadRequest(orders);  
            }
            return Ok(orders);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetOrderByIdAsync([FromRoute] int id)
        {
            var order = await _orderServices.GetOrderByIdAsync(id);
            if(order.Sucesss == false)
            {
                return BadRequest(order);
            }
            return Ok(order);
        }
    }
}
