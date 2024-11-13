using MS3_Back_End.DBContext;

namespace MS3_Back_End.Service
{
    public class StudentService
    {
        private readonly AppDBContext _db;
        public StudentService(AppDBContext db)
        {
            _db = db;
        }
    }
}
