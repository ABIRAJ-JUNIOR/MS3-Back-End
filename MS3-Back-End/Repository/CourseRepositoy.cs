using MS3_Back_End.DBContext;

namespace MS3_Back_End.Repository
{
    public class CourseRepositoy
    {
        private readonly AppDBContext _Db;
        public CourseRepositoy(AppDBContext db)
        {
            _Db = db;
            
        }

       
    }
}
