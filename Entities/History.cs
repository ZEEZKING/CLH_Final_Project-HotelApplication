namespace CLH_Final_Project.Entities
{
    public class History : BaseEntity
    {
       public int BookingId { get; set; }
       public Booking Bookings { get; set; }
    }
}
