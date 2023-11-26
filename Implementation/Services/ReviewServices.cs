using CLH_Final_Project.Dtos;
using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.Entities;
using CLH_Final_Project.Interfaces.Repositories;
using CLH_Final_Project.Interfaces.Services;

namespace CLH_Final_Project.Implementation.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly IReviewRepsoitory _reviewRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReviewServices(IReviewRepsoitory reviewRepository, ICustomerRepository customerRepository)
        {
            _reviewRepository = reviewRepository;
            _customerRepository = customerRepository;
        }

        public async Task<BaseResponse> CreateReview(CreateReviewRequestModel model, int id)
        {
            var customer = await _customerRepository.GetCustomerByUserIdAsync(id);
            if(customer == null)
            {
                return new BaseResponse
                {
                    Message = "Customer Not Found",
                    Sucesss = false
                };
            }
            var review = new Review
            {
                Text = model.Text,
                CustomerId = customer.Id,
                Seen = false,
            };
            await _reviewRepository.CreateAsync(review);
            return new BaseResponse
            {
                Message = "Successfully Commented",
                Sucesss = true
            };
        }

        public async Task<BaseResponse> DeleteReviewAsync(int id)
        {
            var review = await _reviewRepository.GetAsync(id);
            if (review == null)
            {
                return new BaseResponse
                {
                    Message = "Review not Deleted",
                    Sucesss = false
                };
            }
            review.IsDeleted = true;
            await _reviewRepository.UpdateAsync(review);
            return new BaseResponse
            {
                Message = "Review Deleted Successfully",
                Sucesss = true
            };
        }

        public async Task<ReviewsResponseModel> GetAllReviewAsync()
        {
            var review = await _reviewRepository.GetAllReviewsAsync();
            if (review == null)
            {
                return new ReviewsResponseModel
                {
                    Message = "No Review Yet",
                    Sucesss = false
                };
            }
            return new ReviewsResponseModel
            {
                Message = "review Found",
                Sucesss = true,
                Data = review.Select(x => new ReviewDto
                {
                    Id = x.Id,
                    Text = x.Text,
                    Name = x.Customer.User.Name,
                    Image = x.Customer.User.ProfileImage,
                    Seen = x.Seen
                }).ToList(),
            };
        }

        public async Task<ReviewsResponseModel> GetAllReviewsByCustomerAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByUserIdAsync(id);
            if (customer == null)
            {
                return new ReviewsResponseModel
                {
                    Message = "Customer not found",
                    Sucesss = false
                };
            }
            var reviews = await _reviewRepository.GetReviewsByCustomerIdAsync(customer.Id);
            if(reviews.Count == 0)
            {
                return new ReviewsResponseModel
                {
                    Message = "Review not found",
                    Sucesss = false
                };
            }
            return new ReviewsResponseModel
            {
                Message = "Reviews Found",
                Sucesss = true,
                Data = reviews.Select(x => new ReviewDto
                {
                    Id = x.Id,
                    Text = x.Text,
                    Name = x.Customer.User.Name,
                    Image = x.Customer.User.ProfileImage,
                    Seen = x.Seen
                }).ToList(),
            };

        }

        public async Task<ReviewsResponseModel> GetAllUnseenReviewsAsyns()
        {
            var reviews = await _reviewRepository.GetAllUnseenReviewsAsync();
            if (reviews.Count == 0)
            {
                return new ReviewsResponseModel
                {
                    Message = "no unseen review yet",
                    Sucesss = false
                };
            }
            return new ReviewsResponseModel
            {
                Message = "Reviews Found",
                Sucesss = true,
                Data = reviews.Select(x => new ReviewDto
                {
                    Id = x.Id,
                    Text = x.Text,
                    Name = x.Customer.User.Name,
                    Image = x.Customer.User.ProfileImage,
                    Seen = x.Seen
                }).ToList(),
            };
        }

        public async Task<ReviewResponseModel> GetReviewByIdAsync(int id)
        {
            var review = await _reviewRepository.GetReviewById(id);
            if (review == null)
            {
                return new ReviewResponseModel
                {
                    Message = "review not found",
                    Sucesss = false,
                };
            };
            return new ReviewResponseModel
            {
                Message = "Reviews Found",
                Sucesss = true,
                Data = new ReviewDto
                {
                    Id = review.Id,
                    Text = review.Text,
                    Name = review.Customer.User.Name,
                    Image = review.Customer.User.ProfileImage,
                }
            };
        }

        public async Task<BaseResponse> UpdateAll()
        {
            var reviews = await _reviewRepository.GetAllUnseenReviewsAsync(); 
            if(reviews.Count == 0)
            {
                return new BaseResponse
                {
                    Message = "Ni reviews to be updated",
                    Sucesss = false
                };
            }
            foreach (var review in reviews)
            {
                review.Seen = true;
                await _reviewRepository.UpdateAsync(review);
            }
            return new BaseResponse
            {
                Message = "Ypdated Successfuly",
                Sucesss = true,
            };
        }

        public async Task<BaseResponse> UpdateReviewStatusAsync(int id)
        {
            var review = await _reviewRepository.GetAsync(x => x.Id == id);
            if (review == null)
            {
                return new BaseResponse
                {
                    Message = "review not found",
                    Sucesss = false
                };
            }
            review.Seen = true;
            await _reviewRepository.UpdateAsync(review);
            return new BaseResponse
            {
                Message = "Review Updated Successfully",
                Sucesss = true,
            };
        }
    }
}
