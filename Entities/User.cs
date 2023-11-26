using CLH_Final_Project.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLH_Final_Project.Entities
{
    public class User : BaseEntity
    {
       public string? Name { get; set; }
       public string? UserName { get; set; }
       public string? Email{get; set;}
       public string? Password{get; set;}
       public int? Age { get; set;} 
       public string? PhoneNumber{get; set;}
       public string? Address{get; set;}
       public Gender Gender{get; set;}
       public string? Token {get; set;}
       public string? ProfileImage { get; set; }
       public List<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
       public Customer Customer{get; set;}
       public Manager Manager{get; set;}
        

    }
}