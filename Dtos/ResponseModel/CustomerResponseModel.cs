namespace CLH_Final_Project.Dtos.ResponseModel
{
    public class CustomerResponseModel : BaseResponse
    {
        public CustomerDto Data { get; set; }
    }
    public class CustomersResponseModel : BaseResponse
    { 
        public List<CustomerDto> Data { get; set; }
    }
}
