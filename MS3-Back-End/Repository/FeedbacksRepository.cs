using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;

namespace MS3_Back_End.Repository
{
    public class FeedbacksRepository
    {
        private readonly AppDBContext _dbContext;

        public FeedbacksRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Feedbacks> AddFeedbacks(Feedbacks feedbacks) 
        { 
           var data= await _dbContext.Feedbacks.AddAsync(feedbacks);
            _dbContext.SaveChanges();
            return data.Entity;
        }
    }
    
}
