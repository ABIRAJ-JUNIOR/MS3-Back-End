using MS3_Back_End.DTO.RequestDTOs;
using MS3_Back_End.DTO.ResponseDTOs;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<SignUpResponseDTO> SignUp(SignUpRequestDTO request)
        {
            var user = new User()
            {
                Nic = request.Nic,
                Role = Roles.Student,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Email = request.Email,
                Phone = request.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CteatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = true
            };

            var userData = await _userRepository.SignUp(user);

            var response = new SignUpResponseDTO()
            {
                Nic = userData.Nic,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                DateOfBirth = userData.DateOfBirth,
                Gender = userData.Gender,
                Email = userData.Email,
                Phone = userData.Phone,
            };

            return response;
        }
    }
}
