using MS3_Back_End.DBContext;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class NotificationRepository: INotificationRepository
    {
        private readonly AppDBContext _dbContext;

        public NotificationRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
