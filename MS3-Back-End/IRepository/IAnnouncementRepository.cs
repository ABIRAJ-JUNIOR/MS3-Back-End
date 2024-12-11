using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAnnouncementRepository
    {
        Task<Announcement> AddAnnouncement(Announcement AnouncementReq);
        Task<ICollection<Announcement>> SearchAnnouncements(string SearchText);
        Task<ICollection<Announcement>> GetAllAnnouncement();
        Task<Announcement> GetAnnouncemenntByID(Guid AnnouncementId);
        Task<ICollection<Announcement>> RecentAnnouncement(AudienceType Type);
        Task<string> DeleteAnnouncement(Announcement announcement);
        Task<ICollection<Announcement>> GetPaginatedAnnouncement(int pageNumber, int pageSize, string Role);
        Task<ICollection<Announcement>> GetAnnouncementsByRole(string role);

    }
}