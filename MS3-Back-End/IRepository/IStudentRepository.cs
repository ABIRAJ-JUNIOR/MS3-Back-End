using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.Student;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IStudentRepository
    {
        Task<Student> AddStudent(Student StudentReq);
        Task<ICollection<Student>> SearchStudent(string SearchText);
        Task<ICollection<Student>> GetAllStudente();
        Task<Student> GetStudentById(Guid id);
        Task<StudentFullDetailsResponseDTO> GetStudentFullDetailsById(Guid StudentId);
        Task<Student> UpdateStudent(Student Students);
        Task<string> DeleteStudent(Student Student);
        Task<ICollection<StudentWithUserResponseDTO>> GetPaginatedStudent(int pageNumber, int pageSize);
        Task<StudentResponseDTO> UpdateStudentInfoDetails(Guid id, StudentFullUpdateDTO request);
    }
}