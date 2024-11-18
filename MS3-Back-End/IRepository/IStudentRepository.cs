using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IStudentRepository
    {
        Task<Student> AddStudent(Student StudentReq);
        Task<List<Student>> SearchStudent(string SearchText);
        Task<List<Student>> GetAllStudente();
        Task<Student> GetStudentById(Guid StudentId);
        Task<Student> UpdateStudent(Student Students);
        Task<string> DeleteStudent(Student Student);
        Task<ICollection<Student>> GetPaginatedStudent(int pageNumber, int pageSize);
    }
}