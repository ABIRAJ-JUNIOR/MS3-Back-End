using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.ContactUs;
using MS3_Back_End.DTOs.RequestDTOs.Notification;
using MS3_Back_End.DTOs.ResponseDTOs.Notification;
using MS3_Back_End.Entities;
using MS3_Back_End.IService;
using MS3_Back_End.Service;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotificationService notificationService, ILogger<NotificationController> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<NotificationResponseDTO>> AddNotification(NotificationRequest requestDTO)
        {
            if (requestDTO == null)
            {
                return BadRequest("Notification data is required.");
            }

            try
            {
                var message = await _notificationService.AddNotification(requestDTO);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding notification");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{studentId}")]
        public async Task<ActionResult<IEnumerable<NotificationResponseDTO>>> GetAllNotifications(Guid studentId)
        {
            try
            {
                var result = await _notificationService.GetAllNotification(studentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting notifications for student id {studentId}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Read/{id}")]
        public async Task<ActionResult<string>> ReadNotification(Guid id)
        {
            try
            {
                var response = await _notificationService.ReadNotification(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error reading notification with id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<string>> DeleteNotification(Guid id)
        {
            try
            {
                var response = await _notificationService.DeleteNotification(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting notification with id {id}");
                return BadRequest(ex.Message);
            }
        }
    }
}
