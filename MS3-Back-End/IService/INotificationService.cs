using MS3_Back_End.DTOs.RequestDTOs.Notification;
using MS3_Back_End.DTOs.ResponseDTOs.Notification;

namespace MS3_Back_End.IService
{
    public interface INotificationService
    {
        Task<NotificationResponseDTO> AddNotification(NOtificationRequestDTO requestDTO);
        Task<List<NotificationResponseDTO>> GetAllNotification();
        Task<NotificationResponseDTO> GetNotificationById(Guid Id);

    }
}
