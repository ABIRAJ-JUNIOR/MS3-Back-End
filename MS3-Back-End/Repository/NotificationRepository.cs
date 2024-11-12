using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class NotificationRepository: INotificationRepository
    {
        private readonly AppDBContext _dbContext;

        public NotificationRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Notification> AddNotification(Notification notification) 
        {
                 var data= await _dbContext.Notifications.AddAsync(notification);
                  _dbContext.SaveChanges(); 
                    return notification;
        }
    }
}
