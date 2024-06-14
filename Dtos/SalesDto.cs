namespace CLH_Final_Project.Dtos
{
    public class SalesDto
    {
        public double AmountPaid { get; set; }
        public CustomerDto CustomerDto { get; set; }
        public int OrderId { get; set; }
        public List<OrderDto> OrderDto { get; set; }
        public List<BookingDto> BookingDtos { get; set; }
    }
}
