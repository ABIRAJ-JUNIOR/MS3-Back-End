using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class CourseRepositoy : ICourseRepository
    {
        private readonly AppDBContext _Db;
        public CourseRepositoy(AppDBContext db)
        {
            _Db = db;
            
        }

        public async Task<Course> AddCourse(Course courseReq)
        {
            var name = await _Db.Courses.SingleOrDefaultAsync(n=>n.CourseName==courseReq.CourseName);
            if (name == null)
            {
                var data = await _Db.Courses.AddAsync(courseReq);
                await _Db.SaveChangesAsync();
                return data.Entity;
            }
            else
            {
                throw new Exception("Your Course Already Added");
            }
           
        }
        public async Task<ICollection<Course>> SearchCourse(string SearchText)
        {
            var data = await _Db.Courses.Include(cs => cs.CourseSchedules).Where(n=>n.CourseName.Contains(SearchText) || n.Description.Contains(SearchText))
                              .Include(x=>x.CourseSchedules)
                              .Include(x=>x.Feedbacks)
                              .Include(x=>x.Assessments).ToListAsync();
            return data;
        }
        public async Task<ICollection<Course>> GetAllCourse()
        {
            var data = await _Db.Courses.Include(cs => cs.CourseSchedules).Where(c => c.IsDeleted == false)
             .ToListAsync();
            return data;
        }


        public async Task<Course> GetCourseById(Guid CourseId)
        {
            var data = await _Db.Courses.SingleOrDefaultAsync(c=>c.Id==CourseId && c.IsDeleted==false)
                 
            return data;
        }
        public async Task<Course> UpdateCourse(Course course)
        {
            var data =  _Db.Courses.Update(course);
            await _Db.SaveChangesAsync();   
            return data.Entity;
        }
        public async Task<string> DeleteCourse(Course course)
        {
            _Db.Courses.Update(course);
            await _Db.SaveChangesAsync();
            return "Course Deleted Successfull";
        }
        public async Task<ICollection<Course>> GetPaginatedCourses(int pageNumber, int pageSize)
        {
            return await _Db.Courses.Include(cs => cs.CourseSchedules)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
