namespace CLH_Final_Project.Dtos.RequsetModel
{
    public class UpdateRoomRequestModel
    {
        public string RoomName { get; set; }
        public int RoomNumber { get; set; }
        public string Description { get; set; }
        public int Occupancy { get; set; }
        public double Price { get; set; }
        public IFormFile ImagePics { get; set; }
    }
}
