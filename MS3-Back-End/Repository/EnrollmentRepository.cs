using MS3_Back_End.DBContext;

namespace MS3_Back_End.Repository
{
    public class EnrollmentRepository
    {
        private readonly AppDBContext _Db;
        public EnrollmentRepository(AppDBContext db)
        {
            _Db = db;

        }


    }
}
