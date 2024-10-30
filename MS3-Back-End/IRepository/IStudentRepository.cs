using MS3_Back_End.DTO.ResponseDTOs;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IStudentRepository
    {
        Task<ICollection<Student>> GetAllStudents();
        Task<Student> GetStudentByNic(string nic);
    }
}
