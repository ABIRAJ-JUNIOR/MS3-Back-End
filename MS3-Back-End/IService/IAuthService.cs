using MS3_Back_End.DTOs.RequestDTOs.Auth;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IService
{
    public interface IAuthService
    {
        Task<string> SignUp(SignUpRequestDTO request);
        Task<string> SignIn(SignInRequestDTO request);
    }
}
