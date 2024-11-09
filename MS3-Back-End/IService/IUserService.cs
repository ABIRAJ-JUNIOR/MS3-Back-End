using MS3_Back_End.DBContext;
using MS3_Back_End.DTO.RequestDTOs;
using MS3_Back_End.DTO.ResponseDTOs;

namespace MS3_Back_End.IService
{
    public interface IUserService
    {
        Task<SignUpResponseDTO> SignUp(SignUpRequestDTO request);
        Task<string> SignIn(SignInRequestDTO signInRequest);
        Task<ICollection<UserResponseDTO>> GetAllStudent();
    }
}
