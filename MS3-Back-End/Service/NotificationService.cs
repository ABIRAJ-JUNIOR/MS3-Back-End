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

        public async Task<NotificationResponseDTO> AddNotification(NotificationRequestDTO requestDTO )
        {
            var Message = new Notification
            {
                Message = requestDTO.Message,
                NotificationType = requestDTO.NotificationType,
                StudentId = requestDTO.StudentId,
                DateSent = DateTime.Now,
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

        public async Task<List<NotificationResponseDTO>> GetAllNotification()
        {
            var allData = await _notificationRepository.GetAllNotification();
            if (allData == null)
            {
                throw new Exception("No notifications");
            }
            var NotificationResponse = new List<NotificationResponseDTO>();
            foreach (var message in allData)
            {
                var obj = new NotificationResponseDTO
                {
                    Id = message.Id,
                    Message = message.Message,
                    NotificationType = message.NotificationType,
                    DateSent = message.DateSent,
                    StudentId = message.StudentId,
                    IsRead = message.IsRead
                };
                NotificationResponse.Add(obj);
            }
            return NotificationResponse;
        }
        
        public async Task<string> DeleteNotification(Guid id , Notification notification)
        {
            var notificationData = await _notificationRepository.GetNotificationById(id);
            if(notificationData == null)
            {
                throw new Exception("Not found");
            }

            notificationData.IsRead = true;
            await _notificationRepository.DeleteNotification(notification);

            return "Deleted Successfully";
        }
    }
}
