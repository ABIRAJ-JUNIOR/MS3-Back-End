using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDBContext _Db;
        public AnnouncementRepository(AppDBContext db)
        {
            _Db = db;
        }

        public async Task<Announcement> AddAnnouncement(Announcement AnouncementReq)
        {
                var data = await _Db.Announcements.AddAsync(AnouncementReq);
                await _Db.SaveChangesAsync();
                return data.Entity;
        }
        public async Task<ICollection<Announcement>> SearchAnnouncements(string SearchText)
        {
            var data = await _Db.Announcements.Where(n => n.Title.Contains(SearchText)).ToListAsync();
            return data;
        }
        public async Task<ICollection<Announcement>> GetAllAnnouncement()
        {
            var data = await _Db.Announcements.Where(c => c.IsActive == true).ToListAsync();
            return data;
        }
        public async Task<Announcement> GetAnnouncemenntByID(Guid AnnouncementId)
        {
            var data = await _Db.Announcements.SingleOrDefaultAsync(c => c.Id == AnnouncementId && c.IsActive == true);
            return data!;
        }
        public async Task<Announcement> UpdateAnnouncement(Announcement announcement)
        {
            var data = _Db.Announcements.Update(announcement);
            await _Db.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<string> DeleteAnnouncement(Announcement announcement)
        {
            var data = _Db.Announcements.Update(announcement);
            await _Db.SaveChangesAsync();
            return "Delete Announcement SucessFully";
        }
        public async Task<ICollection<Announcement>> GetPaginatedAnnouncement(int pageNumber, int pageSize)
        {
            var data=await _Db.Announcements.Take((pageNumber-1)*pageSize).Skip(pageSize).ToListAsync();
            return data;
        }
    }
}
