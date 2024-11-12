using MS3_Back_End.DTOs.RequestDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.Assessment;

namespace MS3_Back_End.IService
{
    public interface IAssessmentService
    {
        Task<AssessmentResponseDTO> AddAssessment(AssessmentRequestDTO request);
        Task<ICollection<AssessmentResponseDTO>> GetAllAssessment();
        Task<AssessmentResponseDTO> UpdateAssessment(Guid id, UpdateAssessmentRequestDTO request);
    }
}
