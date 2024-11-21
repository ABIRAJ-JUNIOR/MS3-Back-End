using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Course;

namespace MS3_Back_End.Service
{
    public interface ICourseScheduleService
    {
        Task<CourseScheduleResponseDTO> AddCourseSchedule(CourseScheduleRequestDTO courseReq);
        Task<ICollection<CourseScheduleResponseDTO>> SearchCourseSchedule(string SearchText);
        Task<ICollection<CourseScheduleResponseDTO>> GetAllCourseSchedule();
        Task<CourseScheduleResponseDTO> GetCourseScheduleById(Guid CourseId);
        Task<CourseScheduleResponseDTO> UpdateCourseSchedule(UpdateCourseScheduleDTO courseReq);
        Task<PaginationResponseDTO<CourseScheduleResponseDTO>> GetPaginatedCoursesSchedules(int pageNumber, int pageSize);

    }
}