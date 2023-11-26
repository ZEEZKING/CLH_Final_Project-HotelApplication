using CLH_Final_Project.Dtos.ResponseModel;
using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Services
{
    public interface IHistoryServices
    {
        Task<HistoryResponseModel> GetHistoryById(int id);
        Task<HistorysResponseModel> GetAllHistory();
    }
}
