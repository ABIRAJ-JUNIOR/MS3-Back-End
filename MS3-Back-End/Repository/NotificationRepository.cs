using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class NotificationRepository: INotificationRepository
    {
        private readonly AppDBContext _appDBContext;

        public NotificationRepository(AppDBContext _appDBContext)
        {
           this._appDBContext = _appDBContext;
        }

        public async Task<Notification> AddNotification(Notification _notification)
        {
            var notification = await _appDBContext.Notifications.AddAsync(_notification);
            await _appDBContext.SaveChangesAsync();
            return notification.Entity;
        }

        public async Task<ICollection<Notification>> GetAllNotification(Guid id)
        {
            var getAllNotification = await _appDBContext.Notifications.Where(n => n.StudentId == id && n.IsRead==false).ToListAsync();
            return getAllNotification;
        }

        public async Task<Notification> GetNotificationById(Guid Id)
        {
            var getNotificationById = await _appDBContext.Notifications.SingleOrDefaultAsync(C => C.Id == Id);
            return getNotificationById!;
        }

        public async Task<Notification> DeleteNotification(Notification notification)
        {
            var data = _appDBContext.Notifications.Update(notification);
            await _appDBContext.SaveChangesAsync();
            return data.Entity;
        }
    }
}
