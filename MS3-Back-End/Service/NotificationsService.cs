using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;

namespace MS3_Back_End.Service
{
    public class NotificationsService: INotificationsService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationsService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Notification> AddNotification(Notification notification)
        {

        }
    }
}
