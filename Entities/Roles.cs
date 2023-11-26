using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLH_Final_Project.Entities
{
    public class Roles : BaseEntity
    {
        public string Name{ get; set; }
        public string Description { get; set; }
        public List<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}