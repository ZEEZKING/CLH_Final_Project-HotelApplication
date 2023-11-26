using CLH_Final_Project.Enum;

namespace CLH_Final_Project.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime Terminate { get; set; }
        public int Duration { get; set; }
        public BookingStatus Bookings { get; set; } = BookingStatus.pending;
        public string ReferenceNo { get; set; }
        public int Quantity { get; set; }
    }
}
