using Microsoft.EntityFrameworkCore;
using MS3_Back_End.DBContext;
using MS3_Back_End.Entities;
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

        public async Task<ICollection<StudentAssessment>> GetAllAssessments()
        {
           var assessmentList =  await _dbcontext.StudentAssessments.ToListAsync();
           return assessmentList;
        }

        public async Task<ICollection<StudentAssessment>> GetAllNonEvaluateAssessments()
        {
            var assessmentList = await _dbcontext.StudentAssessments.Where(sa => sa.StudentAssessmentStatus != StudentAssessmentStatus.Reviewed && sa.StudentAssessmentStatus != StudentAssessmentStatus.Absent).ToListAsync();
            return assessmentList;
        }

    }
}
