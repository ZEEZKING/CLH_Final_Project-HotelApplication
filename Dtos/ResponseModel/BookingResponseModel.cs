namespace CLH_Final_Project.Dtos.ResponseModel
{
    public class BookingResponseModel : BaseResponse
    {
        public BookingDto Data { get; set; }
    }
    public class BookingsResponseModel : BaseResponse
    { 
        public List<BookingDto> Data { get; set; }
    }
}
