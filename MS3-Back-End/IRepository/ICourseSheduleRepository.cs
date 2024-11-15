using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface ICourseSheduleRepository
    {
        Task<CourseSchedule> AddCourseShedule(CourseSchedule courseReq);
        Task<List<CourseSchedule>> SearchSheduleLocation(string SearchText);
        Task<List<CourseSchedule>> GetAllCourseShedule();
        Task<CourseSchedule> GetCourseSheduleById(Guid id);
        Task<CourseSchedule> UpdateCourseShedule(CourseSchedule course);

    }
}