using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Org.BouncyCastle.Tsp;

namespace CLH_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewServices _reviewServices;

        public ReviewController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }

        [HttpPost("CreateReview/{userId}")]
        public async Task<IActionResult> CreateReview([FromForm]CreateReviewRequestModel model,int userId)
        {
            var review = await _reviewServices.CreateReview(model, userId);
            if(review.Sucesss == false)
            {
                return BadRequest(review);
            }
            return Ok(review);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetReviewId([FromRoute] int id)
        {
            var review = await _reviewServices.GetReviewByIdAsync(id);
            if(review.Sucesss == false)
            {
                return BadRequest(review);
            }
            return Ok(review);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var reviews = await _reviewServices.GetAllReviewAsync();
            if(reviews.Sucesss == false)
            {
                return BadRequest(reviews);
            }
            return Ok(reviews);
        }

        [HttpGet("GetAllUnseenReview")]
        public async Task<IActionResult> GetAllUnseenReviews()
        {
            var reviews = await _reviewServices.GetAllUnseenReviewsAsyns();
            if (reviews.Sucesss == false)
            {
                return BadRequest(reviews);
            }
            return Ok(reviews);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var review = await _reviewServices.UpdateReviewStatusAsync(id);
            if (review.Sucesss == false)
            {
                return BadRequest(review);
            }
            return Ok(review);
        }

        [HttpPut("UpdateAll")]
        public async Task<IActionResult> UpdateAllAsync()
        {
            var review = await _reviewServices.UpdateAll();
            if (review.Sucesss == false)
            {
                return BadRequest(review);
            }
            return Ok(review);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Deleteasync([FromRoute] int id)
        {
            var review = await _reviewServices.DeleteReviewAsync(id);
            if (review.Sucesss == false)
            {
                return BadRequest(review);
            }
            return Ok(review);
        }
    }
}
