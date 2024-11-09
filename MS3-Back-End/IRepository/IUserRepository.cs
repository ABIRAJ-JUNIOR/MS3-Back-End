using MS3_Back_End.DTO.RequestDTOs;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IUserRepository
    {
        Task<User> SignUp(User user);
        Task<User> GetUserByNic(string nic);
        Task<User> Signin(SignInRequestDTO signinRequest);
        Task<ICollection<User>> GetAllStudent();
    }
}
