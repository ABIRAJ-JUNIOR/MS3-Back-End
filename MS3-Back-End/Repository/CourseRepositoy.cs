using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.DTOs.ResponseDTOs.Course;
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
            var courseCategory = await _Db.CourseCategories.SingleOrDefaultAsync(cc => cc.Id == courseReq.CourseCategoryId);
            
            if(courseCategory == null)
            {
                throw new Exception("Course Category not found");
            }

            var data = await _Db.Courses.AddAsync(courseReq);
            await _Db.SaveChangesAsync();
            return data.Entity;
        }
        public async Task<ICollection<Course>> SearchCourse(string SearchText)
        {
            var data = await _Db.Courses.Include(cs => cs.CourseSchedules).Where(n => n.CourseName.Contains(SearchText) || n.Description.Contains(SearchText))
                              .Include(x => x.CourseSchedules)
                              .Include(x => x.Feedbacks).ToListAsync();
            return data;
        }
        public async Task<ICollection<Course>> GetAllCourse()
        {
                var data = await _Db.Courses
            .Include(c => c.CourseSchedules)  
            .Include(c => c.Feedbacks)      
            .Where(c => c.IsDeleted == false) 
            .ToListAsync();

            return data;
        }

        public async Task<Course> GetCourseById(Guid CourseId)
        {
            var data = await _Db.Courses
                                 .Include(c => c.CourseSchedules!)
                                 .Include(c => c.Feedbacks!)
                                 .ThenInclude(f => f.Student)
                                 .SingleOrDefaultAsync(c => c.Id == CourseId && c.IsDeleted == false);

            return data!;
        }
        public async Task<Course> UpdateCourse(Course course)
        {
            var data = _Db.Courses.Update(course);
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
            var courses = await _Db.Courses
                      .Include(c => c.CourseSchedules)     
                      .Include(c => c.Feedbacks)           
                      .Where(c => c.IsDeleted == false)   
                      .Skip((pageNumber - 1) * pageSize)  
                      .Take(pageSize)                     
                      .ToListAsync();

            return courses;
        }

        public async Task<ICollection<Top3CourseDTO>> GetTop3Courses()
        {
            return await _Db.Courses
                .Include(c => c.Feedbacks)
                .Select(c => new Top3CourseDTO()
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    Level = c.Level,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    FeedBackCount = c.Feedbacks!.Count(),
                    FeedBackRate = c.Feedbacks!.Any() ? Math.Round(c.Feedbacks!.Average(f => f.Rating),1) : 0
                })
                .OrderByDescending(c => c.FeedBackRate)
                .ThenByDescending(c => c.FeedBackCount)
                .Take(3)
                .ToListAsync();
        }
    }
}
