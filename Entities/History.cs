namespace CLH_Final_Project.Entities
{
    public class History : BaseEntity
    {
       public int BookingId { get; set; }
       public Booking Bookings { get; set; }
       public int CustomerId { get; set; }
       public Customer Customer { get; set; }

    }
}
