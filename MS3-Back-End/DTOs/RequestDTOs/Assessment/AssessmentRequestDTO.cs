using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Assessment
{
    public class AssessmentRequestDTO
    {
        public Guid CourseId { get; set; }
        public AssessmentType AssessmentType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalMarks { get; set; }
        public int PassMarks { get; set; }
    }
}
