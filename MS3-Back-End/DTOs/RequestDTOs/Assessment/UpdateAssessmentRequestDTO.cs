using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Assessment
{
    public class UpdateAssessmentRequestDTO
    {
        public AssessmentType AssessmentType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalMarks { get; set; }
        public int PassMarks { get; set; }
        public AssessmentStatus Status { get; set; }
        public Guid CourseId { get; set; }
    }
}
