using MS3_Back_End.DTOs.RequestDTOs.Notification;
using MS3_Back_End.DTOs.ResponseDTOs.Notification;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IService
{
    public interface INotificationService
    {
        Task<NotificationResponseDTO> AddNotification(NotificationRequest requestDTO);
        Task<ICollection<NotificationResponseDTO>> GetAllNotification(Guid id);
        Task<string> ReadNotification(Guid id);
        Task<string> DeleteNotification(Guid id);
    }
}
