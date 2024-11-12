using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;

namespace MS3_Back_End.Repository
{
    public class AnnouncementRepository
    {
        private readonly AppDBContext _Db;
        public AnnouncementRepository(AppDBContext db)
        {
            _Db = db;
        }

        public async Task<Announcement> AddAnnouncement(Announcement AnouncementReq)
        {
            var Data = await _Db.Announcements.SingleOrDefaultAsync(n => n.Title==AnouncementReq.Title);
            if (Data == null)
            {
                var data = await _Db.Announcements.AddAsync(AnouncementReq);
                await _Db.SaveChangesAsync();
                return data.Entity;
            }
            else
            {
                throw new Exception("Your Course Already Added");
            }

        }
        public async Task<List<Announcement>> SearchAnnouncements(string SearchText)
        {
            var data = await _Db.Announcements.Where(n => n.Title.Contains(SearchText)).ToListAsync();
            return data;
        }





    }
}
