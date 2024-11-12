using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;

namespace MS3_Back_End.IService
{
    public interface IStudentAssessmentService
    {
        Task<ICollection<StudentAssessmentResponseDTO>> GetAllAssessments();
        Task<ICollection<StudentAssessmentResponseDTO>> GetAllNonEvaluateAssessments();
    }
}
