using MS3_Back_End.DTOs.RequestDTOs.Notification;
using MS3_Back_End.DTOs.ResponseDTOs.Notification;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IService
{
    public interface INotificationService
    {
        Task<NotificationResponseDTO> AddNotification(NotificationRequestDTO requestDTO);
        Task<List<NotificationResponseDTO>> GetAllNotification(Guid id);
        Task<string> DeleteNotification(Guid id);
    }
}
