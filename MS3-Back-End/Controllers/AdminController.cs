using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Image;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs.__Password__;
using MS3_Back_End.DTOs.RequestDTOs.Admin;
using MS3_Back_End.DTOs.ResponseDTOs.Admin;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;
using MS3_Back_End.Service;
using System.Drawing;

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

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAdminFulldetailsById(Guid id)
        {
            try
            {
                var adminData = await _adminService.GetAdminFulldetailsById(id);
                return Ok(adminData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAdmins()
        {
            var adminsList = await _adminService.GetAllAdmins();
            return Ok(adminsList);

        }

        [HttpPut("Update-Full-Details/{id}")]
        public async Task<IActionResult> UpdateAdminFullDetails(Guid id, AdminFullUpdateDTO request)
        {
            var updateresponse = await _adminService.UpdateAdminFullDetails(id, request);
            return Ok(updateresponse);
        }

        [HttpPut("Update-Personal-Details/{id}")]
        public async Task<IActionResult> UpdateAdminPersonalDetails(Guid id, AdminUpdateRequestDTO request)
        {
            try
            {
                var updatedData = await _adminService.UpdateAdminPersonalDetails(id, request);
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

        [HttpPost("Image/{adminId}")]
        public async Task<IActionResult> UploadImage(Guid adminId, IFormFile? ImageFile, bool isCoverImage)
        {
            try
            {
                var response = await _adminService.UploadImage(adminId, ImageFile,isCoverImage);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetPaginatedAdmin(int pageNumber, int pageSize)
        {
            try
            {
                var response = await _adminService.GetPaginatedAdmin(pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")]

        public async Task<IActionResult> DeleteAdmin(Guid Id)
        {
            try
            {
                var rsponse = await _adminService.DeleteAdmin(Id);
                return Ok(rsponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("AdminProfile{ID}")]
        public async Task<IActionResult> UpdateAdminProfile(Guid ID, AdminProfileUpdateDTO admindata)
        {

            try
            {
                var data = await _adminService.UpdateAdminProfile(ID, admindata);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
