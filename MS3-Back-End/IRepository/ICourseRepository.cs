using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface ICourseRepository
    {
        Task<Course> AddCourse(Course courseReq);
        Task<List<Course>> GetAllCourse();
        Task<Course> GetCourseById(Guid CourseId);
        Task<Course> UpdateCourse(Course course);
        Task<string> DeleteCourse(Course course);
    }
}
