using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface ICourseScheduleRepository
    {
        Task<CourseSchedule> AddCourseSchedule(CourseSchedule courseReq);
        Task<ICollection<CourseSchedule>> SearchScheduleLocation(string SearchText);
        Task<ICollection<CourseSchedule>> GetAllCourseSchedule();
        Task<CourseSchedule> GetCourseScheduleById(Guid id);
        Task<CourseSchedule> UpdateCourseSchedule(CourseSchedule course);
        Task<ICollection<CourseSchedule>> GetPaginatedCoursesSchedules(int pageNumber, int pageSize);

    }
}