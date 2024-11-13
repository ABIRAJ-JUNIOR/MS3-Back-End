using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IStudentRepository
    {
        Task<Student> AddStudent(Student StudentReq);
        Task<List<Student>> SearchStudent(string SearchText);
        Task<List<Student>> GetAllStudente();
        Task<Student> GetEnrollmentById(Guid StudentId);
        Task<Student> UpdateStudent(Student Students);

    }
}