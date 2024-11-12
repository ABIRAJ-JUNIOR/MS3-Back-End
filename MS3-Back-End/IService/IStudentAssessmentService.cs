using MS3_Back_End.DTOs.RequestDTOs.StudentAssessment;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;

namespace MS3_Back_End.IService
{
    public interface IStudentAssessmentService
    {
        Task<ICollection<StudentAssessmentResponseDTO>> GetAllAssessments();
        Task<ICollection<StudentAssessmentResponseDTO>> GetAllEvaluatedAssessments();
        Task<ICollection<StudentAssessmentResponseDTO>> GetAllNonEvaluateAssessments();
        Task<string> AddStudentAssessment(StudentAssessmentRequestDTO request);
    }
}
