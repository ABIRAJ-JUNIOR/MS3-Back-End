using MS3_Back_End.DBContext;
using MS3_Back_End.IRepository;

namespace MS3_Back_End.Repository
{
    public class StudentAssessmentRepository : IStudentAssessmentRepository
    {
        private readonly AppDBContext _dbcontext;

        public StudentAssessmentRepository(AppDBContext context)
        {
            _dbcontext = context;
        }
    }
}
