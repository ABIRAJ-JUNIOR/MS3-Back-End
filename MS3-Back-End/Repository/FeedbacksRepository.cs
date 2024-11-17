using MS3_Back_End.DBContext;

namespace MS3_Back_End.Repository
{
    public class FeedbacksRepository
    {
        private readonly AppDBContext _dbContext;

        public FeedbacksRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
    
}
