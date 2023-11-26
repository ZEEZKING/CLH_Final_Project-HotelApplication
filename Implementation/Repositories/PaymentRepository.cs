using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class PaymentRepository : BaseRepository<PaymentReference>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext Context) 
        {
            _Context = Context;
        }
        public async Task<PaymentReference> GetAsync(string transactionReference)
        {
            return await _Context.Payments
            .Include(x => x.Order)
            .Include(x => x.Customer)
            .ThenInclude(x => x.User)
            .Where(x => x.ReferenceNumber == transactionReference).SingleOrDefaultAsync();
        }
    }
}
