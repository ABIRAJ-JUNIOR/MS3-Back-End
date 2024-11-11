using MS3_Back_End.DBContext;
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
    }
}
