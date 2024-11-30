using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class CourseScheduleRepository : ICourseScheduleRepository
    {
        private readonly AppDBContext _Db;
        public CourseScheduleRepository(AppDBContext db)
        {
            _Db = db;

        }

        public async Task<CourseSchedule> AddCourseSchedule(CourseSchedule courseReq)
        {
            var courseData = await _Db.Courses.SingleOrDefaultAsync(c => c.Id == courseReq.CourseId);
            var courseSchedule = await _Db.CourseSchedules.SingleOrDefaultAsync(cs => cs.CourseId == courseReq.CourseId && cs.StartDate == courseReq.StartDate && cs.EndDate == courseReq.EndDate && cs.Location == courseReq.Location);
            if (courseData == null)
            {
                throw new Exception("Course not found");
            }

            if (courseSchedule != null)
            {
                throw new Exception("Course Schedule already exists");
            }
            var data = await _Db.CourseSchedules.AddAsync(courseReq);
            await _Db.SaveChangesAsync();
            return data.Entity;
        }

        public async  Task<ICollection<CourseSchedule>> SearchScheduleLocation(string SearchText)
        {
            var data = await  _Db.CourseSchedules.Where(n => n.Location.Contains(SearchText)).ToListAsync();
            return data;
        }

        public async Task<ICollection<CourseSchedule>> GetAllCourseSchedule()
        {
            var data = await _Db.CourseSchedules.ToListAsync();
            return data;
        }

        public async Task<CourseSchedule> GetCourseScheduleById(Guid id)
        {
            var data = await _Db.CourseSchedules.SingleOrDefaultAsync(c => c.Id == id);
            return data!;
        }

        public async Task<CourseSchedule> UpdateCourseSchedule(CourseSchedule course)
        {
            var data = _Db.CourseSchedules.Update(course);
            await _Db.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<ICollection<CourseSchedule>> GetPaginatedCoursesSchedules(int pageNumber, int pageSize)
        {
            var courses = await _Db.CourseSchedules
                      .Include(c => c.Course).OrderByDescending(c => c.CreatedDate)
                      .Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize)
                      .ToListAsync();

            return courses;
        }
    }
}
