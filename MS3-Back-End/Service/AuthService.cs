using Microsoft.IdentityModel.Tokens;
using MS3_Back_End.DTOs.RequestDTOs.Auth;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MS3_Back_End.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
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
                    var roleData = await _authRepository.GetStudentRole();

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
                        ImagePath = "",
                        CteatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        IsActive = true,
                    };

                    var studentData = await _authRepository.SignUp(student);

                    return "SignUp Successfully";
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

        }

        public async Task<string> SignIn(SignInRequestDTO request)
        {
            var userData = await _authRepository.GetUserByEmail(request.email);

            if(userData == null)
            {
                throw new Exception("User Not Found");
            }

            if(!BCrypt.Net.BCrypt.Verify(request.password, userData.Password))
            {
                throw new Exception("Wrong password.");
            }

            var userRoleData = await _authRepository.GetUserRoleByUserId(userData.Id);
            

            return "Sign In Successfully";
        }

        private string CreateToken(TokenRequestDTO request)
        {
            var claimsList = new List<Claim>();
            //claimsList.Add(new Claim("Id", user.Id.ToString()));
            //claimsList.Add(new Claim("Name", user.Name));
            //claimsList.Add(new Claim("Email", user.Email));
            //claimsList.Add(new Claim("Role", user.Role.ToString()));


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
