using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotification(Notification _notification);
        Task<ICollection<Notification>> GetAllNotification(Guid id);
        Task<Notification> GetNotificationById(Guid Id);
        Task<Notification> ReadNotification(Notification notification);
        Task<Notification> DeleteNotification(Notification notification);
    }
}
