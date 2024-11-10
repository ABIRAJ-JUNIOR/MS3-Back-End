using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;

namespace MS3_Back_End.Repository
{
    public class CourseRepositoy
    {
        private readonly AppDBContext _Db;
        public CourseRepositoy(AppDBContext db)
        {
            _Db = db;
            
        }

        public async Task<Course> AddCourse(Course courseReq)
        {
             var data =await _Db.AddAsync(courseReq);
            await _Db.SaveChangesAsync();
            return data.Entity;
        }

       
    }
}
