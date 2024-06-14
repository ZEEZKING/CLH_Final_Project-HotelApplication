using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLH_Final_Project.Entities
{
    public class Customer : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public List<VerificationCode> VerificationCodes { get; set; } = new List<VerificationCode>();   
        public List<PaymentReference> payments { get; set; } = new List<PaymentReference>();
        public List<Review> Review { get; set; } = new List<Review>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<History> Histories { get; set; } = new List<History>();
    }
}