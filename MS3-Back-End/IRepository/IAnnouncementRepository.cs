using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IAnnouncementRepository
    {
        Task<Announcement> AddAnnouncement(Announcement AnouncementReq);
        Task<ICollection<Announcement>> SearchAnnouncements(string SearchText);
        Task<ICollection<Announcement>> GetAllAnnouncement();
        Task<Announcement> GetAnnouncemenntByID(Guid AnnouncementId);
        Task<Announcement> UpdateAnnouncement(Announcement announcement);
        Task<string> DeleteAnnouncement(Announcement announcement);
    }
}