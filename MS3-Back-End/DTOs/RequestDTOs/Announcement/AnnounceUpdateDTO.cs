using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs
{
    public class AnnounceUpdateDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; }
        public DateTime ExpirationDate { get; set; }
        public AudienceType AudienceType { get; set; }
        public bool IsActive { get; set; }

    }
}
