using CLH_Final_Project.Enum;

namespace CLH_Final_Project.Dtos.RequsetModel
{
    public class CreateCustomerRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
    }
}
