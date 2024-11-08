using MS3_Back_End.DTO.RequestDTOs;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IUserRepository
    {
        Task<User> SignUp(User user);
        Task<User> Signin(SignInRequestDTO signinRequest);
    }
}
