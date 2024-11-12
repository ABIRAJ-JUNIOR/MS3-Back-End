using MS3_Back_End.DBContext;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class AuditLogRepository: IAuditLogRepository
    {
        private readonly AppDBContext _dbContext;

        public AuditLogRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
