using MS3_Back_End.DBContext;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDBContext _dbContext;

        public AuthRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
