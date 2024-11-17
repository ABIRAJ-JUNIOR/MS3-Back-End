using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class FeedbacksRepository: IFeedbacksRepository
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
        public async Task<List<Feedbacks>> getAllFeedbacks()
        {
            var datas= await _dbContext.Feedbacks.ToListAsync();
            return datas;
        }
    }
    
}
