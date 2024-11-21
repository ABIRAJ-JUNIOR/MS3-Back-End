using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment
{
    public class StudentAssessmentResponseDTO
    {
        public Guid Id { get; set; }
        public int? MarksObtaines { get; set; }
        public Grade? Grade { get; set; }
        public string? FeedBack { get; set; } = string.Empty;
        public DateTime DateSubmitted { get; set; }
        public DateTime? DateEvaluated { get; set; }
        public string StudentAssessmentStatus { get; set; } = string.Empty;

        public Guid StudentId { get; set; }
        public Guid AssessmentId { get; set; }
    }
}
