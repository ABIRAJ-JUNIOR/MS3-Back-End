using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly AppDBContext _dbContext;

        public AssessmentRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Assessment> AddAssessment(Assessment assessment)
        {
            var assessmentData = await _dbContext.Assessments.AddAsync(assessment);
            await _dbContext.SaveChangesAsync();
            return assessmentData.Entity;
        }
    }
}
