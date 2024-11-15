using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Announcement
{
    public class AnnouncementRequestDTO
    {
        public string Title { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public AudienceType AudienceType { get; set; }
    }
}
