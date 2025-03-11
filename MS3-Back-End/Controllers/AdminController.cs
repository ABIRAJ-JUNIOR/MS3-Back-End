using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.__Password__;
using MS3_Back_End.DTOs.RequestDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;
using MS3_Back_End.Service;
using NLog;
using System.Drawing;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        public async Task<ActionResult<AdminResponseDTO>> AddAdmin(AdminRequestDTO request)
        {
            try
            {
                var adminData = await _adminService.AddAdmin(request);
                return Ok(adminData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding admin");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<AdminAllDataResponseDTO>> GetAdminFullDetailsById(Guid id)
        {
            try
            {
                var adminData = await _adminService.GetAdminFullDetailsById(id);
                return Ok(adminData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting admin details");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<AdminWithRoleDTO>>> GetAllAdmins()
        {
            try
            {
                var adminsList = await _adminService.GetAllAdmins();
                return Ok(adminsList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all admins");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Full-Details/{id}")]
        public async Task<ActionResult<AdminResponseDTO>> UpdateAdminFullDetails(Guid id, AdminFullUpdateDTO request)
        {
            try
            {
                var updateResponse = await _adminService.UpdateAdminFullDetails(id, request);
                return Ok(updateResponse);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating admin details");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Image/{adminId}/{isCoverImage}")]
        public async Task<ActionResult<string>> UploadImage(Guid adminId, IFormFile? imageFile, bool isCoverImage)
        {
            try
            {
                var response = await _adminService.UploadImage(adminId, imageFile, isCoverImage);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error uploading image");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PaginationResponseDTO<AdminWithRoleDTO>>> GetPaginatedAdmin(int pageNumber, int pageSize)
        {
            try
            {
                var response = await _adminService.GetPaginatedAdmin(pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting paginated admins");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AdminResponseDTO>> DeleteAdmin(Guid id)
        {
            try
            {
                var response = await _adminService.DeleteAdmin(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error deleting admin");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AdminProfile/{id}")]
        public async Task<ActionResult<string>> UpdateAdminProfile(Guid id, AdminProfileUpdateDTO adminData)
        {
            try
            {
                var data = await _adminService.UpdateAdminProfile(id, adminData);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating admin profile");
                return BadRequest(ex.Message);
            }
        }
    }
}
