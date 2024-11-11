using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAuthRepository
    {
        Task<Student> SignUp(Student student);
        Task<User> AddUser(User user);
        Task<Role> GetRole();
        Task<Student> GetStudentByNic(string nic);
        Task<User> GetUserByEmail(string email);
    }
}
