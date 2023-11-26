namespace CLH_Final_Project.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int PackagesId { get; set; }  
        public Packages Packages { get; set; } 
        public Sale Sale { get; set; }
        public PaymentReference Payment { get; set; }

    }
}
