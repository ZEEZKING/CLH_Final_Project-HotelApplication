using CLH_Final_Project.Entities;
using CLH_Final_Project.Entities.Context;
using CLH_Final_Project.Interfaces.Repositories;

namespace CLH_Final_Project.Implementation.Repositories
{
    public class VerificationCodeRepository : BaseRepository<VerificationCode>, IVerificationCodeRepository
    {
        public VerificationCodeRepository(ApplicationDbContext context) 
        { 
            _Context = context;
        }
    }
}
