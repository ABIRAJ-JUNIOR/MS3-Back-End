using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface ICourseSheduleRepository
    {
        Task<CourseSchedule> AddCourseShedule(CourseSchedule courseReq);
        Task<ICollection<CourseSchedule>> SearchSheduleLocation(string SearchText);
        Task<ICollection<CourseSchedule>> GetAllCourseShedule();
        Task<CourseSchedule> GetCourseSheduleById(Guid id);
        Task<CourseSchedule> UpdateCourseShedule(CourseSchedule course);
        Task<ICollection<CourseSchedule>> GetPaginatedCoursesSchedules(int pageNumber, int pageSize);

    }
}