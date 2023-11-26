using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface IReviewServices
    {
        Task<BaseResponse> CreateReview(CreateReviewRequestModel model, int id);
        Task<ReviewsResponseModel> GetAllReviewsByCustomerAsync(int id);
        Task<ReviewsResponseModel> GetAllReviewAsync();
        Task<ReviewsResponseModel> GetAllUnseenReviewsAsyns();
        Task<BaseResponse> UpdateReviewStatusAsync(int id);
        Task<BaseResponse> DeleteReviewAsync(int id);
        Task<ReviewResponseModel> GetReviewByIdAsync(int id);
        Task<BaseResponse> UpdateAll();




    }
}
