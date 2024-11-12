using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs;
using MS3_Back_End.DTOs.RequestDTOs.Announcement;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncement(AnnouncementRequestDTO announcementRequest)
        {
            if (announcementRequest == null)
            {
                return BadRequest("Announcement data is required.");
            }

            try
            {
                var announcement = await _announcementService.AddAnnouncement(announcementRequest);
                return Ok(announcement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            try
            {
                var announcements = await _announcementService.GetAllAnnouncement();
                return Ok(announcements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Announcement{id}")]
        public async Task<IActionResult> GetAnnouncementById(Guid id)
        {
            try
            {
                var announcement = await _announcementService.GetAnnouncementById(id);
                return Ok(announcement);
            }
            catch (Exception ex)
            {
                return NotFound($"Announcement with id {id} not found: {ex.Message}");
            }
        }



    }
}
