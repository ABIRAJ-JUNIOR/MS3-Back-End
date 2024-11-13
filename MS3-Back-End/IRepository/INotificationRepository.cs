using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotification(Notification _notification);
        Task<List<Notification>> GetAllNotification();
    }
}
