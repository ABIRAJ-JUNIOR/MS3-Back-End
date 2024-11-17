using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Notification
{
    public class NotificationRequest
    {
        public string Message { get; set; } = string.Empty;
        public NotificationType NotificationType { get; set; }
        public Guid StudentId { get; set; }
    }
}
