using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingServices _bookingServices;

        public BookingController(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        [HttpPost("CreateBooking/{userId}")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequestModel model, [FromRoute] int userId)
        {
            var booking = await _bookingServices.CreateBooking(model, userId);  
            if(booking.Sucesss == false)
            {
                return BadRequest(booking);
            }
            return Ok(booking);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var bookings = await _bookingServices.GetAllBookings();
            if(bookings.Sucesss == false)
            {
                return BadRequest(bookings);
            }
            return Ok(bookings);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            var booking = await _bookingServices.GetBookingById(id);
            if(booking.Sucesss == false) 
            { 
                return BadRequest(booking); 
            }
            return Ok(booking);
        }

        [HttpGet("Terminate/{id}/{userId}")]
        public async Task<IActionResult> TerminateAsync([FromRoute] int id,[FromRoute]int userId)
        {
            var terminate = await _bookingServices.TerminateBooking(id,userId);
            if(terminate.Sucesss == false)
            {
               return BadRequest(terminate);
            }
            return Ok(terminate);
        }

        [HttpPut("CheckIn")]
        public async Task<IActionResult> CheckInAsync([FromForm]UpdateBookingRequestModel model)
        {
            var booking = await _bookingServices.UpdateCheckIn(model);
            if(booking.Sucesss == false)
            {
                return BadRequest(booking);
            }
            return Ok(booking);
        }

        [HttpPut("CheckOut/{userId}")]
        public async Task<IActionResult> CheckOutAsync([FromForm] UpdateBookingRequestModel model, [FromRoute] int userId)
        {
            var booking = await _bookingServices.UpdateCheckOut(model, userId);
            if(booking.Sucesss == false)
            {
                return BadRequest(booking);
            }
            return Ok(booking);
        }

    }
}
