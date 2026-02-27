using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.__Password__;
using MS3_Back_End.DTOs.RequestDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.AuditLog;
using MS3_Back_End.DTOs.ResponseDTOs.Student;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using Microsoft.SqlServer.Server;
using System.Runtime.InteropServices;
using Azure.Core;
using MS3_Back_End.DTOs.Email;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MS3_Back_End.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthRepository _authRepository;
        private readonly SendMailService _sendMailService;
        private readonly ILogger<AdminService> _logger;

        public AdminService(IAdminRepository adminRepository, IAuthRepository authRepository, SendMailService sendMailService, ILogger<AdminService> logger)
        {
            _adminRepository = adminRepository;
            _authRepository = authRepository;
            _sendMailService = sendMailService;
            _logger = logger;
        }

        public async Task<AdminResponseDTO> AddAdmin(AdminRequestDTO request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var nicCheck = await _adminRepository.GetAdminByNic(request.Nic);
            var emailCheck = await _authRepository.GetUserByEmail(request.Email);

            if (nicCheck != null)
            {
                throw new InvalidOperationException("NIC already used");
            }

            if (emailCheck != null)
            {
                throw new InvalidOperationException("Email already used");
            }

            var user = new User()
            {
                Email = request.Email,
                IsConfirmEmail = false,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            };

            var userData = await _authRepository.AddUser(user);
            var roleData = request.Role == AdminRole.Administrator ? await _authRepository.GetRoleByName("Administrator") : await _authRepository.GetRoleByName("Instructor");

            if (roleData == null)
            {
                throw new InvalidOperationException("Role not found");
            }

            var userRole = new UserRole()
            {
                UserId = userData.Id,
                RoleId = roleData.Id
            };

            await _authRepository.AddUserRole(userRole);

            var admin = new Admin()
            {
                Id = userData.Id,
                Nic = request.Nic,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                ImageUrl = null,
                CreatedDate = DateTime.Now,
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
                ImageUrl = adminData.ImageUrl,
                CreatedDate = adminData.CreatedDate,
                UpdatedDate = adminData.UpdatedDate,
                IsActive = adminData.IsActive,
            };

            var verifyMail = new SendVerifyMailRequest()
            {
                Name = $"{adminData.FirstName} {adminData.LastName}",
                Email = userData.Email,
                VerificationLink = $"http://localhost:4200/email-verified/{userData.Id}",
                EmailType = EmailTypes.EmailVerification,
            };

            await _sendMailService.VerifyMail(verifyMail);

            _logger.LogInformation("Admin added successfully with Id: {Id}", adminData.Id);

            return response;
        }

        public async Task<AdminAllDataResponseDTO> GetAdminFullDetailsById(Guid id)
        {
            var adminData = await _adminRepository.GetAdminFullDetailsById(id);
            if (adminData == null)
            {
                _logger.LogWarning("Admin not found for Id: {Id}", id);
                throw new KeyNotFoundException("Admin not found");
            }
            return adminData;
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
                ImageUrl = a.ImageUrl,
                CreatedDate = a.CreatedDate,
                UpdatedDate = a.UpdatedDate,
                IsActive = a.IsActive,
                AuditLogs = a.AuditLogs?.Select(data => new AuditLogResponseDTO()
                {
                    Id = data.Id,
                    AdminId = data.AdminId,
                    ActionDate = data.ActionDate,
                    Details = data.Details,
                    Action = data.Action,
                    AdminResponse = null!
                }).ToList()
            }).ToList();

            return response;
        }

        public async Task<AdminResponseDTO> UpdateAdminFullDetails(Guid id, AdminFullUpdateDTO request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var adminData = await _adminRepository.GetAdminById(id);

            if (adminData == null)
            {
                _logger.LogWarning("Admin not found for Id: {Id}", id);
                throw new KeyNotFoundException("Admin not found");
            }

            adminData.FirstName = request.FirstName;
            adminData.LastName = request.LastName;
            adminData.Phone = request.Phone;
            adminData.UpdatedDate = DateTime.Now;

            var updatedData = await _adminRepository.UpdateAdmin(adminData);

            var userData = await _authRepository.GetUserById(id);
            if (userData == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (request.Password != null)
            {
                userData.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            await _authRepository.UpdateUser(userData);

            var userRoleData = await _authRepository.GetUserRoleByUserId(id);
            if (userRoleData == null)
            {
                throw new KeyNotFoundException("UserRole not found");
            }

            var roleData = await _authRepository.GetRoleByName(request.Role.ToString());
            if (roleData == null)
            {
                throw new KeyNotFoundException("Role not found");
            }

            userRoleData.RoleId = roleData.Id;

            await _authRepository.UpdateUserRole(userRoleData);

            var response = new AdminResponseDTO()
            {
                Id = updatedData.Id,
                Nic = updatedData.Nic,
                FirstName = updatedData.FirstName,
                LastName = updatedData.LastName,
                Phone = updatedData.Phone,
                ImageUrl = updatedData.ImageUrl,
                CreatedDate = updatedData.CreatedDate,
                UpdatedDate = updatedData.UpdatedDate,
                IsActive = updatedData.IsActive,
            };

            _logger.LogInformation("Admin updated successfully with Id: {Id}", id);

            return response;
        }

        public async Task<string> UploadImage(Guid adminId, IFormFile? imageFile, bool isCoverImage)
        {
            var adminData = await _adminRepository.GetAdminById(adminId);
            if (adminData == null)
            {
                _logger.LogWarning("Admin not found for Id: {Id}", adminId);
                throw new KeyNotFoundException("Admin not found");
            }

            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile), "Image file cannot be null");
            }

            var cloudinaryUrl = "cloudinary://779552958281786:JupUDaXM2QyLcruGYFayOI1U9JI@dgpyq5til";

            Cloudinary cloudinary = new Cloudinary(cloudinaryUrl);

            using (var stream = imageFile.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(imageFile.FileName, stream),
                    UseFilename = true,
                    UniqueFilename = true,
                    Overwrite = true
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                if (isCoverImage)
                {
                    adminData.CoverImageUrl = uploadResult.SecureUrl.ToString();
                }
                else
                {
                    adminData.ImageUrl = uploadResult.SecureUrl.ToString();
                }
            }

            await _adminRepository.UpdateAdmin(adminData);

            _logger.LogInformation("Image uploaded successfully for AdminId: {AdminId}", adminId);

            return "Image uploaded successfully";
        }

        public async Task<PaginationResponseDTO<AdminWithRoleDTO>> GetPaginatedAdmin(int pageNumber, int pageSize)
        {
            var allAdmins = await _adminRepository.GetAllAdmins();
            if (allAdmins == null)
            {
                throw new KeyNotFoundException("Admins not found");
            }

            var admins = await _adminRepository.GetPaginatedAdmin(pageNumber, pageSize);

            var paginationResponseDto = new PaginationResponseDTO<AdminWithRoleDTO>
            {
                Items = admins,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(allAdmins.Count / (double)pageSize),
                TotalItem = allAdmins.Count,
            };

            return paginationResponseDto;
        }

        public async Task<AdminResponseDTO> DeleteAdmin(Guid id)
        {
            var adminData = await _adminRepository.GetAdminById(id);
            if (adminData == null)
            {
                _logger.LogWarning("Admin not found for Id: {Id}", id);
                throw new KeyNotFoundException("Admin not found");
            }

            adminData.IsActive = false;
            var deletedAdmin = await _adminRepository.DeleteAdmin(adminData);

            var response = new AdminResponseDTO()
            {
                Id = deletedAdmin.Id,
                Nic = deletedAdmin.Nic,
                FirstName = deletedAdmin.FirstName,
                LastName = deletedAdmin.LastName,
                Phone = deletedAdmin.Phone,
                ImageUrl = deletedAdmin.ImageUrl,
                CreatedDate = deletedAdmin.CreatedDate,
                UpdatedDate = deletedAdmin.UpdatedDate,
                IsActive = deletedAdmin.IsActive,
            };

            _logger.LogInformation("Admin deleted successfully with Id: {Id}", id);

            return response;
        }

        public async Task<string> UpdateAdminProfile(Guid id, AdminProfileUpdateDTO request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var adminData = await _adminRepository.GetAdminById(id);
            if (adminData == null)
            {
                _logger.LogWarning("Admin not found for Id: {Id}", id);
                throw new KeyNotFoundException("Admin not found");
            }

            adminData.FirstName = request.FirstName;
            adminData.LastName = request.LastName;
            adminData.Phone = request.Phone;
            adminData.UpdatedDate = DateTime.Now;

            await _adminRepository.UpdateAdmin(adminData);

            var userData = await _authRepository.GetUserById(id);
            if (userData == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            userData.Email = request.Email;

            await _authRepository.UpdateUser(userData);

            if (request.NewPassword != null)
            {
                if (request.CurrentPassword == null)
                {
                    throw new InvalidOperationException("Current password is required");
                }
                if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, userData.Password))
                {
                    throw new InvalidOperationException("Current password is incorrect");
                }

                userData.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                await _authRepository.UpdateUser(userData);
            }

            _logger.LogInformation("Admin profile updated successfully for Id: {Id}", id);

            return "Account update successful";
        }
    }
}
