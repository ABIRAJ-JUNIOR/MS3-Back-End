using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
