using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Notification;
using MS3_Back_End.DTOs.ResponseDTOs.Notification;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationsService _notificationsService;

        public NotificationController(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }

        [HttpPost("Add-Notification")]
        public async Task<IActionResult> AddNotification(NotificationRequestDTO notificationRequestDTO) 
        {
            var data = await _notificationsService.AddNotification(notificationRequestDTO);
            return Ok(data);
        
        }
    }
}
