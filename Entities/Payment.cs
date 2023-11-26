namespace CLH_Final_Project.Entities
{
    public class PaymentReference : BaseEntity
    {
        public string ReferenceNumber { get; set; } 
        public double Amount { get; set; }  
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        
    }
}
