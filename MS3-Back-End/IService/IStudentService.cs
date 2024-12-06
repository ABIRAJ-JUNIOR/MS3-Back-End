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
        Task<StudentFullDetailsResponseDTO> GetStudentFullDetailsById(Guid StudentId);
        Task<StudentResponseDTO> UpdateStudentFullDetails(Guid id, StudentFullUpdateDTO request);
        Task<ICollection<StudentResponseDTO>> GetAllStudent();
        Task<StudentResponseDTO> UpdateStudent(StudentUpdateDTO studentUpdate);
        Task<string> DeleteStudent(Guid Id);
        Task<PaginationResponseDTO<StudentWithUserResponseDTO>> GetPaginatedStudent(int pageNumber, int pageSize);
        Task<string> UploadImage(Guid studentId, IFormFile? image);
        Task<StudentResponseDTO> UpdateStudentInfoDetails(Guid id, StudentFullUpdateDTO request);
        Task<string> UpdateStudentPassword(Guid studentId, string password, string confirmPassword);

    }
}