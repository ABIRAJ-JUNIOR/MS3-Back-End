using MS3_Back_End.DBContext;

namespace MS3_Back_End.Repository
{
    public class NotificationRepository
    {
        private readonly AppDBContext _dbContext;

        public NotificationRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
