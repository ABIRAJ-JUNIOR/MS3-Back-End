using MS3_Back_End.DBContext;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDBContext _dbContext;

        public AdminRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
