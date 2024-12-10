using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs;
using MS3_Back_End.DTOs.RequestDTOs.Announcement;
using MS3_Back_End.DTOs.ResponseDTOs.Announcement;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;
using MS3_Back_End.Service;

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
                return BadRequest(ex.Message);
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

        [HttpGet("Recent/{Type}")]
        public async Task<IActionResult> RecentAnnouncement(AudienceType Type)
        {
            try
            {
                var response = await _announcementService.RecentAnnouncement(Type);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(Guid id)
        {
            try
            {
                var result = await _announcementService.DeleteAnnouncement(id);
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAnnouncement(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return BadRequest("Search text is required.");
            }

            try
            {
                var searchResults = await _announcementService.SearchAnnouncement(searchText);
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }

        [HttpGet("Pagination/{pagenumber}/{pagesize}")]
        public async Task<IActionResult> GetPaginatedAnnouncement(int pagenumber,int pagesize,string? role)
        {
            try
            {
                var Anouncements = await _announcementService.GetPaginatedAnnouncement(pagenumber, pagesize, role);
                return Ok(Anouncements);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ValidAnouncements")]
        public async Task<IActionResult> ValidAnnoncement()
        {
            try
            {
                var updatedData = await _announcementService.AnnouncementValidCheck();
                return Ok(updatedData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
