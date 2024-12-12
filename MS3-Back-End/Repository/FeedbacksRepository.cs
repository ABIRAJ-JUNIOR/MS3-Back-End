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
        public async Task<ICollection<Feedbacks>> getAllFeedbacks()
        {
            var datas= await _dbContext.Feedbacks.ToListAsync();
            return datas;
        }

        public async Task<ICollection<Feedbacks>> GetTopFeetbacks()
        {
            var datas = await _dbContext.Feedbacks.Include(s => s.Student).OrderByDescending(f => f.FeedBackDate).Take(4).ToListAsync();
            return datas;
        }

        public async Task<ICollection<Feedbacks>> GetFeedBacksByStudentId(Guid Id)
        {
            var data = await _dbContext.Feedbacks.Where( f => f.StudentId == Id).ToListAsync();
            return data;
        }

        public async Task<ICollection<Feedbacks>> GetPaginatedFeedBack(int pageNumber, int pageSize)
        {
            var feedbacks = await _dbContext.Feedbacks
                      .Include(s => s.Student)
                      .Include(c => c.Course)
                      .OrderByDescending(f => f.FeedBackDate)
                      .Skip((pageNumber - 1) * pageSize)
                      .Take(pageSize)
                      .ToListAsync();

            return feedbacks;
        }
    }
    
}
