using MS3_Back_End.DTOs.Image;
using MS3_Back_End.DTOs.RequestDTOs.__Password__;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminService(IAdminRepository adminRepository, IAuthRepository authRepository, IWebHostEnvironment webHostEnvironment)
        {
            _adminRepository = adminRepository;
            _authRepository = authRepository;
            _webHostEnvironment = webHostEnvironment;
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

        public async Task<AdminResponseDTO> UpdateAdmin(Guid id , AdminUpdateRequestDTO request)
        {
            var adminData = await _adminRepository.GetAdminById(id);
            if(adminData == null)
            {
                throw new Exception("Admin not found");
            }

            adminData.FirstName = request.FirstName;
            adminData.LastName = request.LastName;
            adminData.Phone = request.Phone;
            adminData.UpdatedDate = DateTime.Now;

            var updatedData = await _adminRepository.UpdateAdmin(adminData);

            var response = new AdminResponseDTO()
            {
                Id = updatedData.Id,
                Nic = updatedData.Nic,
                FirstName = updatedData.FirstName,
                LastName = updatedData.LastName,
                Phone = updatedData.Phone,
                ImagePath = updatedData.ImagePath,
                CteatedDate = updatedData.CteatedDate,
                UpdatedDate = updatedData.UpdatedDate,
                IsActive = updatedData.IsActive,
            };

            return response;
        }

        public async Task<string> UpdateEmail(UpdateEmailRequestDTO request)
        {
            var userData = await _adminRepository.GetUserById(request.Id);
            if(userData == null)
            {
                throw new Exception("User not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password , userData.Password))
            {
                throw new Exception("Wrong Password");
            }

            userData.Email = request.Email;

            var updatedData = await _adminRepository.UpdateUser(userData);

            return "Update email successfully";
        }

        public async Task<string> UpdatePassword(UpdatePasswordRequestDTO request)
        {
            var userData = await _adminRepository.GetUserById(request.Id);
            if (userData == null)
            {
                throw new Exception("User not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.oldPassword, userData.Password))
            {
                throw new Exception("Old password is incorrect");
            }

            userData.Password = BCrypt.Net.BCrypt.HashPassword(request.newPassword);
            var updatedData = await _adminRepository.UpdateUser(userData);

            return "Update password successfully";
        }

        public async Task<string> UploadImage(Guid adminId ,ImageRequestDTO request)
        {
            var adminData = await _adminRepository.GetAdminById(adminId);
            if(adminData == null)
            {
                throw new Exception("Admin not found");
            }

            adminData.ImagePath = request.ImageFile != null ? await SaveImageFile(request.ImageFile) : null;
            var updatedData = await _adminRepository.UpdateAdmin(adminData);

            return "Image upload successfully";
        }

        private async Task<string> SaveImageFile(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return string.Empty;

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Admin");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string filePath = Path.Combine(uploadPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return $"/Admin/{fileName}";
        }
    }
}
