using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesServices _salesServices;

        public SalesController(ISalesServices salesServices)
        {
            _salesServices = salesServices;
        }

        /* [HttpGet("GetByName")]
         public async Task<IActionResult> GetByNameAsync([FromQuery] string name)
         {
             var sales = await _salesServices.GetSalesByCustomerNameAsync(name);
             if (sales.Sucesss == false)
             {
                 return BadRequest(sales);
             }
             return Ok(sales);
         }*/

        [HttpPost("CreateSales/{id}/{bookingId}")]

        public async Task<IActionResult> CreateSales([FromRoute]int id,[FromRoute]int bookingId)
        {
            var sales = await _salesServices.CreateSales(id, bookingId);
            if (sales.Sucesss == false)
            {
                return BadRequest(sales);
            }
            return Ok(sales);

        }
        
       
        [HttpGet("GetAllSales")]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _salesServices.GetAllSales();
            if (sales.Sucesss == false)
            {
                return BadRequest(sales);
            }
            return Ok(sales);
        }
        [HttpGet("GetThisYearSales")]
        public async Task<IActionResult> GetThisYearSales()
        {
            var sales = await _salesServices.GetSalesForThisYear();
            if (sales.Sucesss == false)
            {
                return BadRequest(sales);
            }
            return Ok(sales);
        }
        [HttpGet("GetThisMonthSales")]
        public async Task<IActionResult> GetThisMonthSales()
        {
            var sales = await _salesServices.GetSalesForThisMonth();
            if (sales.Sucesss == false)
            {
                return BadRequest(sales);
            }
            return Ok(sales);
        }
       
       
        
       
        [HttpGet("CalculateAllMonthlySales/{year}")]
        public async Task<IActionResult> CalculateAllMonthlySalesAsync(int year)
        {
            var sales = await _salesServices.CalculateAllMonthlySalesAsync(year);
            if (sales.Sucesss == false)
            {
                return BadRequest(sales);
            }
            return Ok(sales);
        }
       
       

    }
}
