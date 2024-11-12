using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.StudentAssessment
{
    public class StudentAssessmentRequestDTO
    {
        public DateTime DateSubmitted { get; set; }
        public StudentAssessmentStatus StudentAssessmentStatus { get; set; }
        public Guid StudentId { get; set; }
        public Guid AssessmentId { get; set; }
    }
}
