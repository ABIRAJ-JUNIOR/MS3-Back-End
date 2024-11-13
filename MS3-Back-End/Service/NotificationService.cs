using MS3_Back_End.DTOs.RequestDTOs.ContactUs;
using MS3_Back_End.DTOs.RequestDTOs.Notification;
using MS3_Back_End.DTOs.ResponseDTOs.ContactUs;
using MS3_Back_End.DTOs.ResponseDTOs.Notification;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;
using MS3_Back_End.IService;
using MS3_Back_End.Repository;
using System.Data;

namespace MS3_Back_End.Service
{
    public class NotificationService: INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationResponseDTO> AddNotification(NOtificationRequestDTO requestDTO )
        {
            var Message = new Notification
            {
                Message = requestDTO.Message,
                NotificationType = requestDTO.NotificationType,
                IsRead = false
            };

            var data = await _notificationRepository.AddNotification(Message);

            var newNotification = new NotificationResponseDTO
            {
                Id = data.Id,
                Message = data.Message,
                NotificationType = data.NotificationType,
                DateSent = data.DateSent,
                StudentId = data.StudentId,
                IsRead = data.IsRead
            };
            return newNotification;
        }
    }
}
