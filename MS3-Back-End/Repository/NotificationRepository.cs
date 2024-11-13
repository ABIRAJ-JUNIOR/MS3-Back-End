using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class NotificationRepository: INotificationRepository
    {
        private readonly AppDBContext appDBContext;

        public NotificationRepository(AppDBContext _appDBContext)
        {
            appDBContext = _appDBContext;
        }

        public async Task<Notification> AddNotification(Notification _notification)
        {
            var notification = await appDBContext.Notifications.AddAsync(_notification);
            await appDBContext.SaveChangesAsync();
            return notification.Entity;
        }
    }
}
