using Microsoft.IdentityModel.Tokens;
using MS3_Back_End.DTOs.RequestDTOs.Auth;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS3_Back_End.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly INotificationRepository _notificationRepository;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration, INotificationRepository notificationRepository)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            this._notificationRepository = notificationRepository;
        }

        public async Task<string> SignUp(SignUpRequestDTO request)
        {
            var nicCheck = await _authRepository.GetStudentByNic(request.Nic);
            var emailCheck = await _authRepository.GetUserByEmail(request.Email);

            if (nicCheck != null)
            {
                throw new Exception("Nic already used");
            }
            if (emailCheck != null)
            {
                throw new Exception("Email already used");
            }

            var user = new User()
            {
                Email = request.Email,
                IsConfirmEmail = false,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),

            };

            var userData = await _authRepository.AddUser(user);
            var roleData = await _authRepository.GetRoleByName("Student");
            if (roleData == null)
            {
                throw new Exception("Role not found");
            }

            var userRole = new UserRole()
            {
                UserId = userData.Id,
                RoleId = roleData.Id
            };

            var userRoleData = await _authRepository.AddUserRole(userRole);

            var student = new Student()
            {
                Id = userData.Id,
                Nic = request.Nic,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Phone = request.Phone,
                ImageUrl = "",
                CteatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = true,
            };

            var studentData = await _authRepository.SignUp(student);

            string NotificationMessage = $@"
Subject: 🎉 Welcome to Way Makers!<br><br>

Dear {studentData.FirstName} {studentData.LastName},<br><br>

We are thrilled to welcome you to Way Makers, where learning meets excellence!<br><br>

As a valued student, you now have access to:<br>
✅ A wide range of industry-relevant courses.<br>
✅ Expert instructors to guide your learning journey.<br><br>

Here’s how to get started:<br>
1. Log in to your account using your credentials.<br>
2. Explore available courses.<br>
3. Stay updated with announcements.<br><br>

We are committed to empowering your educational journey and helping you achieve your goals.<br><br>

For assistance, feel free to contact us at info.way.mmakers@gmail.com or call 0702274212.<br><br>

Once again, welcome to the Way Makers family! 🎓<br><br>

Warm regards,<br>
Way Makers<br>
Empowering learners, shaping futures.
";

            var Message = new Notification
            {
                Message = NotificationMessage,
                NotificationType = NotificationType.WelCome,
                StudentId = studentData.Id,
                DateSent = DateTime.Now,
                IsRead = false
            };

            await _notificationRepository.AddNotification(Message);

            return "SignUp Successfully";

        }

        public async Task<string> SignIn(SignInRequestDTO request)
        {
            var userData = await _authRepository.GetUserByEmail(request.email);

            if (userData == null)
            {
                throw new Exception("User Not Found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.password, userData.Password))
            {
                throw new Exception("Wrong password.");
            }

            var userRoleData = await _authRepository.GetUserRoleByUserId(userData.Id);
            var roleData = await _authRepository.GetRoleById(userRoleData.RoleId);
            if (roleData == null)
            {
                throw new Exception("Role not found");
            }

            if (roleData.Name == "Student")
            {
                var studentData = await _authRepository.GetStudentById(userData.Id);
                if(studentData.IsActive == false)
                {
                    throw new Exception("Account Deactivated");
                }
                var tokenRequest = new TokenRequestDTO()
                {
                    Id = studentData.Id,
                    Name = studentData.FirstName,
                    Email = userData.Email,
                    Role = roleData.Name
                };

                return CreateToken(tokenRequest);
            }
            else if (roleData.Name == "Administrator" || roleData.Name == "Instructor")
            {
                var adminData = await _authRepository.GetAdminById(userData.Id);
                if (adminData.IsActive == false)
                {
                    throw new Exception("Account Deactivated");
                }

                var tokenRequest = new TokenRequestDTO()
                {
                    Id = adminData.Id,
                    Name = adminData.FirstName,
                    Email = userData.Email,
                    Role = roleData.Name
                };
                return CreateToken(tokenRequest);
            }

            return null!;
        }

        private string CreateToken(TokenRequestDTO request)
        {
            var claimsList = new List<Claim>();
            claimsList.Add(new Claim("Id", request.Id.ToString()));
            claimsList.Add(new Claim("Name", request.Name));
            claimsList.Add(new Claim("Email", request.Email));
            claimsList.Add(new Claim("Role", request.Role.ToString()));


            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!));
            var credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims: claimsList,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credintials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
