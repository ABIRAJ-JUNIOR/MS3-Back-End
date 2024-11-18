using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class CourseSheduleRepository : ICourseSheduleRepository
    {
        private readonly AppDBContext _Db;
        public CourseSheduleRepository(AppDBContext db)
        {
            _Db = db;

        }

        public async Task<CourseSchedule> AddCourseShedule(CourseSchedule courseReq)
        {
            var courseData = await _Db.Courses.SingleOrDefaultAsync(c => c.Id == courseReq.CourseId);
            if (courseData == null)
            {
                throw new Exception("Course not found");
            }

            var courseSheduleData = await _Db.CourseSchedules.SingleOrDefaultAsync(cs => cs.CourseId == courseReq.CourseId);
            var data = await _Db.CourseSchedules.AddAsync(courseReq);
            await _Db.SaveChangesAsync();
            return data.Entity;
        }

        public async  Task<ICollection<CourseSchedule>> SearchSheduleLocation(string SearchText)
        {
            var data = await  _Db.CourseSchedules.Where(n => n.Location.Contains(SearchText)).ToListAsync();
            return data;
        }

        public async Task<ICollection<CourseSchedule>> GetAllCourseShedule()
        {
            var data = await _Db.CourseSchedules.ToListAsync();
            return data;
        }

        public async Task<CourseSchedule> GetCourseSheduleById(Guid id)
        {
            var data = await _Db.CourseSchedules.SingleOrDefaultAsync(c => c.Id == id);
            return data!;
        }

        public async Task<CourseSchedule> UpdateCourseShedule(CourseSchedule course)
        {
            var data = _Db.CourseSchedules.Update(course);
            await _Db.SaveChangesAsync();
            return data.Entity;
        }
       
    }
}
