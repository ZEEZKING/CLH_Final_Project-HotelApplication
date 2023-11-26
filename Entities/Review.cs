using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLH_Final_Project.Entities
{
    public class Review : BaseEntity
    {
        public string Text { get; set; }
        public bool Seen { get; set; } = false;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}