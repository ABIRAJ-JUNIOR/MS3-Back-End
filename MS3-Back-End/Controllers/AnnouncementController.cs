using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.Pagination;
using MS3_Back_End.DTOs.RequestDTOs;
using MS3_Back_End.DTOs.RequestDTOs.Announcement;
using MS3_Back_End.DTOs.ResponseDTOs.Announcement;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;
using MS3_Back_End.Service;
using NLog;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpPost]
        public async Task<ActionResult<AnnouncementResponseDTO>> AddAnnouncement(AnnouncementRequestDTO announcementRequest)
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
                _logger.Error(ex, "Error adding announcement");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementResponseDTO>>> GetAllAnnouncements()
        {
            try
            {
                var announcements = await _announcementService.GetAllAnnouncement();
                return Ok(announcements);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all announcements");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Announcement/{id}")]
        public async Task<ActionResult<AnnouncementResponseDTO>> GetAnnouncementById(Guid id)
        {
            try
            {
                var announcement = await _announcementService.GetAnnouncementById(id);
                return Ok(announcement);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error getting announcement with id {id}");
                return NotFound($"Announcement with id {id} not found: {ex.Message}");
            }
        }

        [HttpGet("Recent/{type}")]
        public async Task<ActionResult<IEnumerable<AnnouncementResponseDTO>>> RecentAnnouncement(AudienceType type)
        {
            try
            {
                var response = await _announcementService.RecentAnnouncement(type);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting recent announcements");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteAnnouncement(Guid id)
        {
            try
            {
                var result = await _announcementService.DeleteAnnouncement(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error deleting announcement with id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<AnnouncementResponseDTO>>> SearchAnnouncement(string searchText)
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
                _logger.Error(ex, "Error searching announcements");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Pagination/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<PaginationResponseDTO<AnnouncementResponseDTO>>> GetPaginatedAnnouncement(int pageNumber, int pageSize, string? role)
        {
            try
            {
                var announcements = await _announcementService.GetPaginatedAnnouncement(pageNumber, pageSize, role);
                return Ok(announcements);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting paginated announcements");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ValidAnnouncements")]
        public async Task<ActionResult<string>> ValidAnnouncement()
        {
            try
            {
                var updatedData = await _announcementService.AnnouncementValidCheck();
                return Ok(updatedData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error validating announcements");
                return BadRequest(ex.Message);
            }
        }
    }
}
