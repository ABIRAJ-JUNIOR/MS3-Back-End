using MS3_Back_End.DTO.RequestDTOs.UserDTOs;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IUserRepository
    {
        Task<User> SignUp(User user);
        Task<User> GetUserByNic(string nic);
        Task<User> Signin(SignInRequestDTO signinRequest);
        Task<ICollection<User>> GetAllStudent();
        Task<ICollection<User>> GetAllInstructors();
        Task<User> GetStudentById(Guid id);
    }
}
