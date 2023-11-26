using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLH_Final_Project.Entities
{
    public class Manager : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
    }
}