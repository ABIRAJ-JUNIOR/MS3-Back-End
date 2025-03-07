using Microsoft.IdentityModel.Tokens;
using MS3_Back_End.DTOs.Email;
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
        private readonly SendMailService _sendMailService;
        private readonly ILogger<AuthService> _logger;

        private const string StudentRole = "Student";
        private const string AdminRole = "Administrator";
        private const string InstructorRole = "Instructor";

        public AuthService(IAuthRepository authRepository, IConfiguration configuration, INotificationRepository notificationRepository, SendMailService sendMailService, ILogger<AuthService> logger)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _notificationRepository = notificationRepository;
            _sendMailService = sendMailService;
            _logger = logger;
        }

        public async Task<string> SignUp(SignUpRequestDTO request)
        {
            await ValidateSignUpRequest(request);

            var user = new User
            {
                Email = request.Email,
                IsConfirmEmail = false,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            };

            var userData = await _authRepository.AddUser(user);
            var roleData = await _authRepository.GetRoleByName(StudentRole) ?? throw new RoleNotFoundException(StudentRole);

            var userRole = new UserRole
            {
                UserId = userData.Id,
                RoleId = roleData.Id
            };

            await _authRepository.AddUserRole(userRole);

            var student = new Student
            {
                Id = userData.Id,
                Nic = request.Nic,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Phone = request.Phone,
                ImageUrl = "",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = true,
            };

            var studentData = await _authRepository.SignUp(student);

            await SendWelcomeNotification(studentData);
            await SendVerificationEmail(userData, studentData);

            _logger.LogInformation("User signed up successfully with Id: {Id}", userData.Id);

            return "SignUp Successfully";
        }

        public async Task<string> SignIn(SignInRequestDTO request)
        {
            var userData = await _authRepository.GetUserByEmail(request.email) ?? throw new UserNotFoundException(request.email);
            var studentData = await _authRepository.GetStudentById(userData.Id);

            if (!userData.IsConfirmEmail)
            {
                await SendVerificationEmail(userData, studentData);
                throw new EmailNotVerifiedException();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.password, userData.Password))
            {
                throw new InvalidPasswordException();
            }

            var userRoleData = await _authRepository.GetUserRoleByUserId(userData.Id);
            var roleData = await _authRepository.GetRoleById(userRoleData.RoleId) ?? throw new RoleNotFoundException(userRoleData.RoleId.ToString());

            _logger.LogInformation("User signed in successfully with Id: {Id}", userData.Id);

            return roleData.Name switch
            {
                StudentRole => await HandleStudentSignIn(studentData, userData, roleData),
                AdminRole or InstructorRole => await HandleAdminOrInstructorSignIn(userData, roleData),
                _ => throw new RoleNotFoundException(roleData.Name)
            };
        }

        public async Task<string> EmailVerify(Guid userId)
        {
            var userData = await _authRepository.GetUserById(userId) ?? throw new UserNotFoundException(userId.ToString());
            userData.IsConfirmEmail = true;
            await _authRepository.UpdateUser(userData);

            _logger.LogInformation("Email verified successfully for UserId: {UserId}", userId);

            return "Email Verified Successfully";
        }

        private async Task ValidateSignUpRequest(SignUpRequestDTO request)
        {
            if (await _authRepository.GetStudentByNic(request.Nic) != null)
            {
                throw new NicAlreadyUsedException(request.Nic);
            }

            if (await _authRepository.GetUserByEmail(request.Email) != null)
            {
                throw new EmailAlreadyUsedException(request.Email);
            }
        }

        private async Task SendWelcomeNotification(Student studentData)
        {
            string notificationMessage = $@"
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

            var message = new Notification
            {
                Message = notificationMessage,
                NotificationType = NotificationType.Welcome,
                StudentId = studentData.Id,
                DateSent = DateTime.Now,
                IsRead = false
            };

            await _notificationRepository.AddNotification(message);
        }

        private async Task SendVerificationEmail(User userData, Student studentData)
        {
            var verifyMail = new SendVerifyMailRequest
            {
                Name = $"{studentData.FirstName} {studentData.LastName}",
                Email = userData.Email,
                VerificationLink = $"http://localhost:4200/email-verified/{userData.Id}",
                EmailType = EmailTypes.EmailVerification,
            };

            await _sendMailService.VerifyMail(verifyMail);
        }

        private async Task<string> HandleStudentSignIn(Student studentData, User userData, Role roleData)
        {
            if (!studentData.IsActive)
            {
                throw new AccountDeactivatedException();
            }

            var tokenRequest = new TokenRequestDTO
            {
                Id = studentData.Id,
                Name = studentData.FirstName,
                Email = userData.Email,
                Role = roleData.Name
            };

            return CreateToken(tokenRequest);
        }

        private async Task<string> HandleAdminOrInstructorSignIn(User userData, Role roleData)
        {
            var adminData = await _authRepository.GetAdminById(userData.Id);
            if (!adminData.IsActive)
            {
                throw new AccountDeactivatedException();
            }

            var tokenRequest = new TokenRequestDTO
            {
                Id = adminData.Id,
                Name = adminData.FirstName,
                Email = userData.Email,
                Role = roleData.Name
            };

            return CreateToken(tokenRequest);
        }

        private string CreateToken(TokenRequestDTO request)
        {
            var claimsList = new List<Claim>
                {
                    new Claim("Id", request.Id.ToString()),
                    new Claim("Name", request.Name),
                    new Claim("Email", request.Email),
                    new Claim("Role", request.Role)
                };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims: claimsList,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            _logger.LogInformation("Token created successfully for UserId: {UserId}", request.Id);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // Custom exceptions
    public class NicAlreadyUsedException : Exception
    {
        public NicAlreadyUsedException(string nic) : base($"NIC '{nic}' is already used.") { }
    }

    public class EmailAlreadyUsedException : Exception
    {
        public EmailAlreadyUsedException(string email) : base($"Email '{email}' is already used.") { }
    }

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string identifier) : base($"User with identifier '{identifier}' not found.") { }
    }

    public class RoleNotFoundException : Exception
    {
        public RoleNotFoundException(string role) : base($"Role '{role}' not found.") { }
    }

    public class EmailNotVerifiedException : Exception
    {
        public EmailNotVerifiedException() : base("Email is not verified. Please verify your email to proceed.") { }
    }

    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base("Wrong password.") { }
    }

    public class AccountDeactivatedException : Exception
    {
        public AccountDeactivatedException() : base("Account is deactivated.") { }
    }
}