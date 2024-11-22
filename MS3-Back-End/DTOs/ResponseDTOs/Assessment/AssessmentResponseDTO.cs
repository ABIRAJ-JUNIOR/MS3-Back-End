using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.Assessment
{
    public class AssessmentResponseDTO
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string AssessmentType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalMarks { get; set; }
        public int PassMarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Status { get; set; } = string.Empty;

        public CourseResponseDTO courseResponse { get; set; } = new CourseResponseDTO();
        public ICollection<StudentAssessmentResponseDTO> studentAssessmentResponses { get; set; } = new List<StudentAssessmentResponseDTO>();

    }
}
