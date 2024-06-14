using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryServices _history;
        public HistoryController(IHistoryServices history)
        {
            _history = history;
        }

        [HttpGet("Get/{id}")]

        public async Task<IActionResult> GetHistory([FromRoute] int id) 
        {
            var history = await _history.GetHistoryById(id);
            if(history.Sucesss == false)
            {
                return BadRequest(history);
            }
            return Ok(history);
        }

        [HttpGet("GetAllHistory")]

        public async Task<IActionResult> GetAllHistory()
        {
            var historys = await _history.GetAllHistory();
            if(historys.Sucesss == false)
            {
                return BadRequest(historys);
            }
            return Ok(historys);
        }

        [HttpGet("GetHistory/{customerId}")]

        public async Task<IActionResult> GetCustomerHistory([FromRoute] int customerId)
        {
            var history = await _history.GetHistoryByCustomerId(customerId);
            if (history.Sucesss == false)
            {
                return BadRequest(history);
            }
            return Ok(history);
        }
    }
}
