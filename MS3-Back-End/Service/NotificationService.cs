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
        private readonly IStudentRepository _studentRepository;

        public NotificationService(INotificationRepository notificationRepository, IStudentRepository studentRepository)
        {
            _notificationRepository = notificationRepository;
            _studentRepository = studentRepository;
        }

        public async Task<NotificationResponseDTO> AddNotification(NotificationRequestDTO requestDTO )
        {
            var studentData = await _studentRepository.GetStudentById( requestDTO.StudentId );
            if( studentData == null)
            {
                throw new Exception("Student not found");
            }

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

        public async Task<List<NotificationResponseDTO>> GetAllNotification(Guid id)
        {
            var allData = await _notificationRepository.GetAllNotification(id);
            if (allData == null)
            {
                throw new Exception("No notifications");
            }
            
            var NotificationResponse = allData.Select(message => new NotificationResponseDTO()
            {
                Id = message.Id,
                Message = message.Message,
                NotificationType = message.NotificationType,
                DateSent = message.DateSent,
                StudentId = message.StudentId,
                IsRead = message.IsRead
            }).ToList();

            return NotificationResponse;
        }
        
        public async Task<string> DeleteNotification(Guid id)
        {
            var notificationData = await _notificationRepository.GetNotificationById(id);
            if(notificationData == null)
            {
                throw new Exception("Not found");
            }

            notificationData.IsRead = true;
            await _notificationRepository.DeleteNotification(notificationData);

            return "Deleted Successfully";
        }
    }
}
