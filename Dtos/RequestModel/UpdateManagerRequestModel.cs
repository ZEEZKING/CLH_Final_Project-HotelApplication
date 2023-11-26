namespace CLH_Final_Project.Dtos.RequsetModel
{
    public class UpdateManagerRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string Address { get; set; }
    }
}
