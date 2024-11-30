using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Assessment
{
    public class UpdateAssessmentRequestDTO
    {
        public Guid CourseId { get; set; }
        public string AssessmentTitle { get; set; } = string.Empty;
        public AssessmentType AssessmentType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalMarks { get; set; }
        public int PassMarks { get; set; }
        public string? AssessmentLink { get; set; } = string.Empty;
        public AssessmentStatus AssessmentStatus { get; set; }
    }
}
