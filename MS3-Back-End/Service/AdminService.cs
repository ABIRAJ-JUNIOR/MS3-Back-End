using MS3_Back_End.DTOs.RequestDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthRepository _authRepository;

        public AdminService(IAdminRepository adminRepository, IAuthRepository authRepository)
        {
            _adminRepository = adminRepository;
            _authRepository = authRepository;
        }

        public async Task<AdminResponseDTO> AddAdmin(AdminRequestDTO request)
        {
            var nicCheck = await _adminRepository.GetAdminByNic(request.Nic);
            var emailCheck = await _authRepository.GetUserByEmail(request.Email);

            if(nicCheck != null)
            {
                throw new Exception("Nic already used");
            }

            if(emailCheck != null)
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
            var roleData = request.Role == AdminRole.Administrator ? await _authRepository.GetRoleByName("Administrator") : await _authRepository.GetRoleByName("Instructor");
            
            if(roleData == null)
            {
                throw new Exception("Role not found");
            }


            var userRole = new UserRole()
            {
                UserId = userData.Id,
                RoleId = roleData.Id
            };

            var userRoleData = await _authRepository.AddUserRole(userRole);

            var admin = new Admin()
            {
                Id = userData.Id,
                Nic = request.Nic,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                CteatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = true,
            };

            var adminData = await _adminRepository.AddAdmin(admin);

            var response = new AdminResponseDTO()
            {
                Id = adminData.Id,
                Nic = adminData.Nic,
                FirstName = adminData.FirstName,
                LastName = adminData.LastName,
                Phone = adminData.Phone,
                ImagePath = adminData.ImagePath,
                CteatedDate = adminData.CteatedDate,
                UpdatedDate = adminData.UpdatedDate,
                IsActive = adminData.IsActive,
            };

            return response;
        }

        public async Task<ICollection<AdminResponseDTO>> GetAllAdmins()
        {
            var adminsList = await _adminRepository.GetAllAdmins();

            var response = adminsList.Select(a => new AdminResponseDTO()
            {
                Id = a.Id,
                Nic = a.Nic,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Phone = a.Phone,
                ImagePath = a.ImagePath,
                CteatedDate = a.CteatedDate,
                UpdatedDate = a.UpdatedDate,
                IsActive = a.IsActive,
            }).ToList();

            return response;
        }

        public async Task<AdminResponseDTO> UpdateAdmin(AdminRequestDTO request)
        {
            
        }
    }
}
