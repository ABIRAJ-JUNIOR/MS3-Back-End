using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAnnouncementRepository
    {
        Task<Announcement> AddAnnouncement(Announcement AnouncementReq);
        Task<List<Announcement>> SearchAnnouncements(string SearchText);
        Task<List<Announcement>> GetAllAnnouncement();
        Task<Announcement> GetAnnouncemenntByID(Guid AnnouncementId);
        Task<Announcement> UpdateAnnouncement(Announcement announcement);
        Task<Announcement> DeleteAnnouncement(Announcement announcement);
    }
}