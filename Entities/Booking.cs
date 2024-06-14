using CLH_Final_Project.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLH_Final_Project.Entities
{
    public class Booking : BaseEntity
    {
        public DateTime CheckIn{get; set;}
        public DateTime CheckOut{get; set;}
        public DateTime Terminate { get; set;}  
        public int Duration{get; set;}
        public BookingStatus Bookings{get; set;} = BookingStatus.pending;
        public string ReferenceNo{get; set;}
        public int Quantity { get; set; }
        public int RoomId{get; set;}
        public Room Room{get; set;} 
        public PaymentReference Payment { get; set;}
        public History History { get; set;}
        public Sale Sale { get; set;}

        
    }
}