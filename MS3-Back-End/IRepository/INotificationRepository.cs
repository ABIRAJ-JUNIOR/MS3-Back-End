using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface INotificationRepository
    {
         Task<Notification> AddNotification(Notification notification);
         Task<List<Notification>> GetNotificationBYStuID(Guid Id);

        Task<Notification> GetNotificationbyID(Guid Id);
         Task<Notification> updatenotification(Notification notification);




    }
}
