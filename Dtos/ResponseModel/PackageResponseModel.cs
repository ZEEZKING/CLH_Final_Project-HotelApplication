namespace CLH_Final_Project.Dtos.ResponseModel
{
    public class PackageResponseModel : BaseResponse
    {
        public PackagesDto Data { get; set; }
    }

    public class PackagesResponseModel : BaseResponse
    {
        public List<PackagesDto> Data { get; set; }
    }
}
