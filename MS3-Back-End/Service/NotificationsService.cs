using MS3_Back_End.IRepository;

namespace MS3_Back_End.Service
{
    public class NotificationsService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationsService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
    }
}
