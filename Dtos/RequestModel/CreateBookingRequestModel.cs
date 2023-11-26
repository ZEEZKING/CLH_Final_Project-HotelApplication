using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Dtos.RequsetModel
{
    public class CreateBookingRequestModel
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Duration { get; set; }
        public List<BookingItems> BookingItems { get; set; } = new List<BookingItems>();


    }

    public class BookingItems
    {
        public int Quantity { get; set; }
        public int  RoomId { get; set; }
    }
}
