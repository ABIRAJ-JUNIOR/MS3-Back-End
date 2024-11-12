using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using System.Runtime.InteropServices;

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
        public async Task<List<Notification>> GetNotificationBYStuID(Guid Id)
        {
            var datas= _dbContext.Notifications.Where(a=>a.StudentId==Id).ToList();
            return datas;
        }
        public async Task<Notification> GetNotificationbyID(Guid Id)
        {

            var data =await _dbContext.Notifications.SingleOrDefaultAsync(a => a.Id == Id);
            return data;
        }
        public async Task<Notification> updatenotification(Notification notification)
        {
             var data= _dbContext.Notifications.Update(notification);
            _dbContext.SaveChanges();
            return notification;
        }
    }
}
