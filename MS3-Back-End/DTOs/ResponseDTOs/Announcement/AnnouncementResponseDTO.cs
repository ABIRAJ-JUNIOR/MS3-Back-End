using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.Announcement
{
    public class AnnouncementResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string AudienceType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
