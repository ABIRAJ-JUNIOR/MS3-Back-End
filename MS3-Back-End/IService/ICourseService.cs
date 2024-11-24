using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Course;

namespace MS3_Back_End.IService
{
    public interface ICourseService
    {
        Task<CourseResponseDTO> AddCourse(CourseRequestDTO courseReq);
        Task<ICollection<CourseResponseDTO>> SearchCourse(string SearchText);
        Task<ICollection<CourseResponseDTO>> GetAllCourse();
        Task<CourseResponseDTO> GetCourseById(Guid CourseId);
        Task<CourseResponseDTO> UpdateCourse(UpdateCourseRequestDTO course);
        Task<string> DeleteCourse(Guid Id);
        Task<PaginationResponseDTO<CourseResponseDTO>> GetPaginatedCourses(int pageNumber, int pageSize);
        Task<string> UploadImage(Guid CourseId, IFormFile image);

    }
}
