using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface IHistoryRepository : IBaseRepository<History>
    {
        Task<History> GetHistoryById(int id);
        Task<List<History>> GetAllHistorys();
        Task<History>  GetHistoryByCustomerId(int customerId);
    }
}
