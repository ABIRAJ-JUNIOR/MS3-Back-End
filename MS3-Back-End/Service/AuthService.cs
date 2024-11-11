using MS3_Back_End.DTOs.RequestDTOs.Auth;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using System.Data;

namespace MS3_Back_End.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<string> SignUp(SignUpRequestDTO request)
        {
            var nicCheck = await _authRepository.GetStudentByNic(request.Nic);
            var emailCheck = await _authRepository.GetUserByEmail(request.Email);

            if(nicCheck == null)
            {
                if(emailCheck == null)
                {
                    var user = new User()
                    {
                        Email = request.Email,
                        IsConfirmEmail = false,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.Password),

                    };

                    var userData = await _authRepository.AddUser(user);
                    var roleData = await _authRepository.GetRole();

                    var userRole = new UserRole()
                    {
                        UserId = userData.Id,
                        RoleId = roleData.Id
                    };

                    var userRoleData = await _authRepository.AddUserRole(userRole);

                    var student = new Student()
                    {
                        Nic = request.Nic,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        DateOfBirth = request.DateOfBirth,
                        Gender = request.Gender,
                        Phone = request.Phone,
                        ImagePath = "",
                        CteatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IsActive = true,
                        UserRoleId = userRoleData.Id
                    };
                }
                else
                {
                    throw new Exception("Email already used");
                }
            }
            else
            {
                throw new Exception("Nic already used");
            }

            return "Successfully SignUp";
        }
    }
}
