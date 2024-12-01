using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.Notification
{
    public class NotificationResponseDTO
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime DateSent { get; set; }
        public string NotificationType { get; set; } = string.Empty;
        public bool IsRead { get; set; }

        public Guid StudentId { get; set; }


    }
}
