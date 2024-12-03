using System.Reflection;

namespace MS3_Back_End.Entities
{
    public class Announcement
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; }
        public DateTime ExpirationDate { get; set; }
        public AudienceType AudienceType { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public enum AudienceType
    {
        Admins = 1,
        Students = 2,
        Everyone = 3
    }
}
