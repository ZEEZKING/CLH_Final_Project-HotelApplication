namespace CLH_Final_Project.Dtos.ResponseModel
{
    public class OrderResponseModel : BaseResponse
    {
        public OrderDto Data { get; set; }
    }

    public class OrdersResponseModel : BaseResponse
    {
        public List<OrderDto> Data { get; set; }
    }

}
