using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAuthRepository
    {
        Task<Student> SignUp(Student student);
        Task<User> AddUser(User user);
        Task<Role> GetRoleByName(string name);
        Task<UserRole> AddUserRole(UserRole userRole);
        Task<Student> GetStudentByNic(string nic);
        Task<User> GetUserByEmail(string email);

        Task<User> UpdateUser(User user);
        Task<UserRole> UpdateUserRole(UserRole userRole);
        Task<User> GetUserById(Guid id);
        Task<UserRole> GetUserRoleByUserId(Guid userId);
        Task<Student> GetStudentById(Guid id);
        Task<Role> GetRoleById(Guid id);
        Task<Admin> GetAdminById(Guid id);
    }
}
