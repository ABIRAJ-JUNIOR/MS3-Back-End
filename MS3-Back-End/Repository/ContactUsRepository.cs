using MS3_Back_End.DBContext;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class ContactUsRepository: IContactUsRepository
    {
        public readonly AppDBContext _dbContext;

        public ContactUsRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
