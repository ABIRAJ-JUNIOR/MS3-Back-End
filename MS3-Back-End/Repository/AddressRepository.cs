using MS3_Back_End.DBContext;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class AddressRepository: IAddressRepository
    {
        private readonly AppDBContext _dbContext;

        public AddressRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        
    }
}
