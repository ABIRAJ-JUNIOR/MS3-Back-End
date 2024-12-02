using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
using MS3_Back_End.Entities;

namespace MS3_Back_End.IRepository
{
    public interface IStudentAssessmentRepository
    {
        Task<ICollection<StudentAssessmentResponseDTO>> GetAllAssessments();
        Task<ICollection<StudentAssessmentResponseDTO>> GetAllEvaluatedAssessments();
        Task<ICollection<StudentAssessmentResponseDTO>> GetAllNonEvaluateAssessments();
        Task<StudentAssessment> AddStudentAssessment(StudentAssessment studentAssessment);
        Task<List<StudentAssessment>> GetStudentAssesmentById(Guid studentId);
        Task<StudentAssessment> EvaluateStudentAssessment(StudentAssessment studentAssessment);
        Task<StudentAssessment> StudentAssessmentGetById(Guid id);
    }
}
