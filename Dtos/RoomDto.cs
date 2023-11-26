namespace CLH_Final_Project.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public int RoomNumber { get; set; }
        public string Description { get; set; }
        public int Occupancy { get; set; }
        public double price { get; set; }
        public bool IsAvailable { get; set; }
        public string Image { get; set; }
        public string Types { get; set; }
        public int Quantity { get; set; }
    }
}
