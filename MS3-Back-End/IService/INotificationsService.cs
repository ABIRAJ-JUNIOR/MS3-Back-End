using MS3_Back_End.DTOs.RequestDTOs.Notification;
using MS3_Back_End.DTOs.ResponseDTOs.Notification;

namespace MS3_Back_End.IService
{
    public interface INotificationsService

    {
         Task<NotificationResponceDTO> AddNotification(NotificationRequestDTO notificationDTO);
         Task<List<NotificationResponceDTO>> GetNotificationBYStuID(Guid Id);
         Task<NotificationResponceDTO> GetNotificationbyID(Guid Id);

         Task<NotificationResponceDTO> updateIsread(Guid Id);

        Task<NotificationResponceDTO> DeleteNotification(Guid id);




    }
}
