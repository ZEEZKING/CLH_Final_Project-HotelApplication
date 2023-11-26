using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayStackPayment _payStackPayment;

        public PaymentController(IPayStackPayment payStackPayment)
        {
            _payStackPayment = payStackPayment;
        }

        /* [HttpPost("InitiatePayment/{userid}/{bookingId}/{orderId?}")]
         public async Task<IActionResult> MakePayMent([FromBody] CreatePaymentRequestModel model, [FromRoute] int userid, [FromRoute] int bookingId, [FromRoute] int? orderId = null)
         {
             var payment = await _payStackPayment.InitiatePayment(model, userid, bookingId, orderId);
             return Ok(payment);
         }*/

        [HttpPost("InitiatePayment/{userid}/{bookingId}")]
        public async Task<IActionResult> MakePayMent(
             [FromBody] CreatePaymentRequestModel model,
             [FromRoute] int userid,
             [FromRoute] int bookingId,
             [FromQuery] int? orderId = null)
        {
            var payment = await _payStackPayment.InitiatePayment(model, userid, bookingId, orderId);
            if(payment != null)
            {
                return Ok(payment);
            }
            return BadRequest(payment);
        }


        [HttpGet("Get/{transactionReference}")]
        public async Task<IActionResult> GetTransactionReceipt([FromRoute] string transactionReference)
        {
            var transaction = await _payStackPayment.GetTransactionRecieptAsync(transactionReference);
            if (transaction == null || transaction == null)
            {
                return BadRequest(transaction);
            }
            return new OkObjectResult(transaction);
        }
    }
}
