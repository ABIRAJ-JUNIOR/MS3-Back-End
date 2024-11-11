using MS3_Back_End.DTOs.RequestDTOs.Auth;

namespace MS3_Back_End.IService
{
    public interface IAuthService
    {
        Task<string> SignUp(SignUpRequestDTO request);
    }
}
