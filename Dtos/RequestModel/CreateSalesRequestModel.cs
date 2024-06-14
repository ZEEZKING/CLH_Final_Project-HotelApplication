namespace CLH_Final_Project.Dtos.RequestModel
{
    public class CreateSalesRequestModel
    {
        public double AmountPaid { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int BookingId { get; set; }
    }
}
