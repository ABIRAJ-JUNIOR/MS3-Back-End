using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Course;

namespace MS3_Back_End.Service
{
    public interface ICourseSheduleService
    {
        Task<CourseSheduleResponseDTO> AddCourseShedule(CourseSheduleRequestDTO courseReq);
        Task<ICollection<CourseSheduleResponseDTO>> SearchCourseShedule(string SearchText);
        Task<ICollection<CourseSheduleResponseDTO>> GetAllCourseShedule();
        Task<CourseSheduleResponseDTO> GetCourseSheduleById(Guid CourseId);
        Task<CourseSheduleResponseDTO> UpdateCourseShedule(UpdateCourseSheduleDTO courseReq);
        Task<PaginationResponseDTO<CourseSheduleResponseDTO>> GetPaginatedCoursesSchedules(int pageNumber, int pageSize);

    }
}