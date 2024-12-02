using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface ICourseRepository
    {
        Task<Course> AddCourse(Course courseReq);
        Task<ICollection<Course>> SearchCourse(string SearchText);
        Task<ICollection<Course>> GetAllCourse();
        Task<Course> GetCourseById(Guid CourseId);
        Task<Course> UpdateCourse(Course course);
        Task<string> DeleteCourse(Course course);
        Task<ICollection<Course>> GetPaginatedCourses(int pageNumber, int pageSize);
        Task<ICollection<Top3CourseDTO>> GetTop3Courses();
    }
}
