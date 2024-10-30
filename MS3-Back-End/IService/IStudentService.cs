using MS3_Back_End.DTO.RequestDTOs;
using MS3_Back_End.DTO.ResponseDTOs;

namespace MS3_Back_End.IService
{
    public interface IStudentService
    {
        Task<ICollection<StudentResponseDTO>> GetAllStudents();
        Task<StudentResponseDTO> GetStudentByNic(string nic);
        Task<StudentResponseDTO> AddStudent(StudentRequestDTO studentRequest);
    }
}
