namespace CLH_Final_Project.Entities
{
    public class VerificationCode : BaseEntity
    {
        public int Code { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
