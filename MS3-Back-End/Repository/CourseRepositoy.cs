using Microsoft.EntityFrameworkCore;
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
             var data =await _Db.Courses.AddAsync(courseReq);
            await _Db.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<List<Course>> GetAllCourse()
        {
            var data = await _Db.Courses.ToListAsync();
            return data;
        }

        public async Task<Course> GetCourseById(Guid CourseId)
        {
            var data = await _Db.Courses.SingleOrDefaultAsync(c=>c.Id==CourseId);
            return data;
        }
        public async Task<Course> UpdateCourse(Course course)
        {
            var data =  _Db.Courses.Update(course);
            await _Db.SaveChangesAsync();   
            return data.Entity;
        }
       
    }
}
