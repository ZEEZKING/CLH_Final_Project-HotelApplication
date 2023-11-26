namespace CLH_Final_Project.Dtos.ResponseModel
{
    public class ManagerResponseModel : BaseResponse
    {
        public ManagerDto Data { get; set; }
    }
    public class ManagersResponseModel : BaseResponse
    { 
        public List<ManagerDto> Data { get; set;}
    }
}
