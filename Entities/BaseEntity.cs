using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLH_Final_Project.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime DateCreated{ get; set; } = DateTime.Now;
    }
}