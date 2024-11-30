using Microsoft.EntityFrameworkCore;
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

        public async  Task<ICollection<Assessment>> GetAllAssessment()
        {
            var assessmentList = await _dbContext.Assessments.ToListAsync();
            return assessmentList!;
        }

        public async Task<Assessment> UpdateAssessment(Assessment assessment)
        {
            var assessmentData = _dbContext.Assessments.Update(assessment);
            await _dbContext.SaveChangesAsync();
            return assessmentData.Entity;
        }

        public  async Task<Assessment> GetAssessmentById(Guid id)
        {
            var assessmentData = await _dbContext.Assessments.SingleOrDefaultAsync(a => a.Id == id);
            return assessmentData!;
        }

        public async Task<ICollection<Assessment>> GetPaginatedAssessment(int pageNumber, int pageSize)
        {
            var assessment = await _dbContext.Assessments.OrderByDescending(o => o.CreatedDate)
                .Include(c => c.Course)
                .Include(sa => sa.StudentAssessments)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return assessment;
        }
    }
}
