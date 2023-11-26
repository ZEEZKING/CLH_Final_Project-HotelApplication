namespace CLH_Final_Project.Entities
{
    public class Sale : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public double AmountPaid { get; set; }
    }
}
