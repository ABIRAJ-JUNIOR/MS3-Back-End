using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IStudentAssessmentRepository
    {
        Task<ICollection<StudentAssessment>> GetAllAssessments();
        Task<ICollection<StudentAssessment>> GetAllEvaluatedAssessments();
        Task<ICollection<StudentAssessment>> GetAllNonEvaluateAssessments();
        Task<StudentAssessment> AddStudentAssessment(StudentAssessment studentAssessment);
        Task<StudentAssessment> EvaluateStudentAssessment(StudentAssessment studentAssessment);
        Task<StudentAssessment> StudentAssessmentGetById(Guid id);
    }
}
