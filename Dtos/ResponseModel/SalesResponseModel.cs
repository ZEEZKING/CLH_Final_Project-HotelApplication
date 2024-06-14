namespace CLH_Final_Project.Dtos.ResponseModel
{
    
        public class SaleResponseModel : BaseResponse
        {
            public SalesDto Data { get; set; }
        }

        public class SalesResponseModel : BaseResponse
        {
            public List<SalesDto> Data { get; set; }
        }
}
