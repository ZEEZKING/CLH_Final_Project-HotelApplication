using CLH_Final_Project.Dtos.RequsetModel;
using CLH_Final_Project.Dtos.ResponseModel;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface IBookingServices
    {
        Task<BookingsResponseModel> CreateBooking(CreateBookingRequestModel model,int userId);
        Task<BookingResponseModel> GetBookingById(int id);
        Task<BookingResponseModel> UpdateCheckIn(UpdateBookingRequestModel model);
        Task<BookingResponseModel> UpdateCheckOut(UpdateBookingRequestModel model,int userId);
        Task<BaseResponse> TerminateBooking(int id,int userId);
        Task<BookingsResponseModel> GetAllBookings();
    }
}
