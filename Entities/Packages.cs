using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLH_Final_Project.Entities
{
    public class Packages : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Types{ get; set; }
        public double Price { get; set; }
        public string Images { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}