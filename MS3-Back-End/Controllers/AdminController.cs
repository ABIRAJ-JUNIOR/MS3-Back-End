using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Image;
using MS3_Back_End.DTOs.RequestDTOs.__Password__;
using MS3_Back_End.DTOs.RequestDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminRequestDTO request)
        {
            try
            {
                var adminData = await _adminService.AddAdmin(request);
                return Ok(adminData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            var adminsList = await _adminService.GetAllAdmins();
            return Ok(adminsList);
        }

        [HttpPut("Update-Personal-Details")]
        public async Task<IActionResult> UpdateAdmin(Guid id, AdminUpdateRequestDTO request)
        {
            try
            {
                var updatedData = await _adminService.UpdateAdmin(id, request);
                return Ok(updatedData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Email")]
        public async Task<IActionResult>  UpdateEmail(UpdateEmailRequestDTO request)
        {
            try
            {
                var updateEmail = await _adminService.UpdateEmail(request);
                return Ok(updateEmail);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordRequestDTO request)
        {
            try
            {
                var updatePassword = await _adminService.UpdatePassword(request);
                return Ok(updatePassword);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Image")]
        public async Task<IActionResult> UploadImage(Guid adminId, ImageRequestDTO request)
        {
            try
            {
                var response = await _adminService.UploadImage(adminId, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
