using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.StudentAssessment
{
    public class StudentAssessmentRequestDTO
    {
        public Guid StudentId { get; set; }
        public Guid AssessmentId { get; set; }
    }
}
