using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.Notification
{
    public class NotificationResponceDTO
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime DateSent { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsRead { get; set; }

        public Guid StudentId { get; set; }


        public Student? Student { get; set; }
    }
}
