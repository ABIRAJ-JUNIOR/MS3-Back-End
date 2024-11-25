using MS3_Back_End.DTOs.Image;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.IService
{
    public interface IStudentService
    {
        Task<StudentResponseDTO> AddStudent(StudentRequestDTO StudentReq);
        Task<ICollection<StudentResponseDTO>> SearchStudent(string SearchText);
        Task<StudentResponseDTO> GetStudentById(Guid StudentId);
        Task<ICollection<StudentResponseDTO>> GetAllStudent();
        Task<StudentResponseDTO> UpdateStudent(StudentUpdateDTO studentUpdate);
        Task<string> DeleteStudent(Guid Id);
        Task<PaginationResponseDTO<StudentResponseDTO>> GetPaginatedStudent(int pageNumber, int pageSize);
        Task<string> UploadImage(Guid studentId, IFormFile? image);
    }
}