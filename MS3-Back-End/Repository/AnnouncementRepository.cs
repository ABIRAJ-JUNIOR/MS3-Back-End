using MS3_Back_End.DBContext;

namespace MS3_Back_End.Repository
{
    public class AnnouncementRepository
    {
        private readonly AppDBContext _Db;
        public AnnouncementRepository(AppDBContext db)
        {
            _Db = db;
        }
    }
}
