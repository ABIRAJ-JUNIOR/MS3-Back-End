
using MS3_Back_End.DBContext;

namespace MS3_Back_End.Repository
{
    public class OtpRepository
    {
        private readonly AppDBContext _Db;
        public OtpRepository(AppDBContext db)
        {
            _Db = db;
        }

      

    }
}
