namespace CLH_Final_Project.Dtos.ResponseModel
{
    public class HistoryResponseModel : BaseResponse
    {
        public HistoryDto Data { get; set; }
    }

    public class HistorysResponseModel : BaseResponse
    {
        public List<HistoryDto> Data { get; set;}
    }

}
