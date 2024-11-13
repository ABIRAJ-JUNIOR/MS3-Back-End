using MS3_Back_End.DBContext;

namespace MS3_Back_End.Repository
{
    public class StudentRepository 
    {
        private readonly AppDBContext _db;
        public StudentRepository(AppDBContext db)
        {
            _db = db;
        }

    }
}
