using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Course;

namespace MS3_Back_End.Service
{
    public interface ICourseSheduleService
    {
        Task<CourseSheduleResponseDTO> AddCourseShedule(CourseSheduleRequestDTO courseReq);
        Task<List<CourseSheduleResponseDTO>> SearchCourse(string SearchText);
        Task<List<CourseSheduleResponseDTO>> GetAllCourse();
        Task<CourseSheduleResponseDTO> GetCourseById(Guid CourseId);
        Task<CourseSheduleResponseDTO> UpdateCourse(UpdateCourseSheduleDTO courseReq);

    }
}