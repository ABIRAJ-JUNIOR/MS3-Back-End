using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
