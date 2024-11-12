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

        public async Task<ICollection<StudentAssessment>> GetAllEvaluatedAssessments()
        {
            var assessmentList = await _dbcontext.StudentAssessments.Where(sa => sa.StudentAssessmentStatus == StudentAssessmentStatus.Reviewed).ToListAsync();
            return assessmentList;
        }

        public async Task<ICollection<StudentAssessment>> GetAllNonEvaluateAssessments()
        {
            var assessmentList = await _dbcontext.StudentAssessments.Where(sa => sa.StudentAssessmentStatus != StudentAssessmentStatus.Reviewed).ToListAsync();
            return assessmentList;
        }

        public async Task<StudentAssessment> AddStudentAssessment(StudentAssessment studentAssessment)
        {
            var assessmentData = await _dbcontext.StudentAssessments.AddAsync(studentAssessment);
            await _dbcontext.SaveChangesAsync();
            return assessmentData.Entity;
        }

        public async Task<StudentAssessment> EvaluateStudentAssessment(StudentAssessment studentAssessment)
        {
            var updatedData =  _dbcontext.StudentAssessments.Update(studentAssessment);
            await _dbcontext.SaveChangesAsync();
            return updatedData.Entity;
        }

    }
}
