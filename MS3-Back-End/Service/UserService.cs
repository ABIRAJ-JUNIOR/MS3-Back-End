using MS3_Back_End.DTO.RequestDTOs.UserDTOs;
using MS3_Back_End.DTO.ResponseDTOs.UserResponseDTOs;
using MS3_Back_End.DTOs.ResponseDTOs.UserResponseDTOs;
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
            var student = await _userRepository.GetUserByNic(request.Nic);
            if (student == null)
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
                    ImagePath = "",
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
            else
            {
                throw new Exception("Nic already used");
            }
            
        }

        public async Task<string> SignIn(SignInRequestDTO signInRequest)
        {
            var userData = await _userRepository.Signin(signInRequest);

            if (userData == null)
            {
                throw new Exception("User not found");
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(signInRequest.Password, userData.Password);
            if (isValidPassword)
            {
                if (userData.Role == Roles.Admin)
                {
                    return "Admin";
                }
                else if(userData.Role == Roles.Instructor)
                {
                    return "Instructor";
                }
                else if(userData.Role == Roles.Student)
                {
                    return "Student";
                }
            }
            else
            {
                throw new Exception("Wrong Password");
            }

            return null!;
        }

        public async Task<ICollection<UserResponseDTO>> GetAllStudent()
        {
            var userList = await _userRepository.GetAllStudent();

            var response = new List<UserResponseDTO>();
            foreach (var user in userList)
            {
                response.Add(new UserResponseDTO() { 
                    Id = user.Id,
                    Nic = user.Nic,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    Email = user.Email,
                    Phone = user.Phone,
                    ImagePath = user.ImagePath,
                });
            }

            return response;
        }

        public async Task<ICollection<UserResponseDTO>> GetAllInstructors()
        {
            var userList = await _userRepository.GetAllInstructors();

            var response = userList.Select(I => new UserResponseDTO()
            {
                Id = I.Id,
                Nic = I.Nic,
                FirstName = I.FirstName,
                LastName = I.LastName,
                DateOfBirth = I.DateOfBirth,
                Gender = I.Gender,
                Email = I.Email,
                Phone = I.Phone,
                ImagePath = I.ImagePath,
            }).ToList();

            return response;
        }
    }
}
