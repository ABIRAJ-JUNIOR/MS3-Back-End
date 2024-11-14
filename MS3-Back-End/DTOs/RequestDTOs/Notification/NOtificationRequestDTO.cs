using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Notification
{
    public class NOtificationRequestDTO
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateOnly DateSent { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsRead { get; set; }

        public Guid StudentId { get; set; }
    }
}
