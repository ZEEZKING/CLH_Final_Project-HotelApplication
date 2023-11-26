using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLH_Final_Project.Entities
{
    public class Room : BaseEntity
    {
       public string RoomName{get; set;}
       public int RoomNumber{get; set;}
       public string Description { get; set;}
       public string Types { get; set; }
       public int Quantity { get; set; }
       public int Occupancy { get; set;}   
       public double Price{get; set;}
       public string RoomImage { get; set;}
       public bool IsAvailable{get; set;}
       public List<Booking> Bookings{get; set;}

    }
}