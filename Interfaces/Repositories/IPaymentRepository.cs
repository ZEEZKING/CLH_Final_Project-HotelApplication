using CLH_Final_Project.Entities;

namespace CLH_Final_Project.Interfaces.Repositories
{
    public interface IPaymentRepository : IBaseRepository<PaymentReference>
    {
        Task<PaymentReference> GetAsync(string transactionReference);

    }
}
