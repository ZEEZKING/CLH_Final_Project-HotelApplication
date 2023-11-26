namespace CLH_Final_Project.Dtos.RequsetModel
{
    public class CreatePackagesRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Types { get; set; }
        public double Price { get; set; }
        public IFormFile PackageImage { get; set; }
    }
}
