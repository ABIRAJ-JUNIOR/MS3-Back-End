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
            try
            {
                var data = await _notificationsService.AddNotification(notificationRequestDTO);
                return Ok(data);
            }
            catch (Exception ex) 
            {
              return BadRequest(ex.Message);
            }
        
        }
        [HttpGet("GetNotificationBY-StuID")]
        public async Task<IActionResult> GetNotificationbystuID(Guid id)
        {
            try
            {
                var data = await _notificationsService.GetNotificationBYStuID(id);
                return Ok(data);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetNotificationby-ID")]
        public async Task<IActionResult> GetNotificationbyID(Guid Id)
        {
            try
            {
                var data = _notificationsService.GetNotificationbyID(Id);
                return Ok(data);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("update-Isread")]
        public async Task<IActionResult> updateIsread(Guid Id)
        {
            var data=await _notificationsService.updateIsread(Id);
            return Ok(data);
        }


    }
}
