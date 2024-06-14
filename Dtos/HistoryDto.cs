using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Dtos
{
    public class HistoryDto
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public BookingDto BookingDtos { get; set; }
    }
}
