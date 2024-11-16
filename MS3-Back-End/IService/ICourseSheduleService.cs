using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Course;

namespace MS3_Back_End.Service
{
    public interface ICourseSheduleService
    {
        Task<CourseSheduleResponseDTO> AddCourseShedule(CourseSheduleRequestDTO courseReq);
        Task<List<CourseSheduleResponseDTO>> SearchCourseShedule(string SearchText);
        Task<List<CourseSheduleResponseDTO>> GetAllCourseShedule();
        Task<CourseSheduleResponseDTO> GetCourseSheduleById(Guid CourseId);
        Task<CourseSheduleResponseDTO> UpdateCourseShedule(UpdateCourseSheduleDTO courseReq);

    }
}