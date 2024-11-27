using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.Course
{
    public class CourseScheduleResponseDTO
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public int Duration { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int MaxStudents { get; set; }
        public int EnrollCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string ScheduleStatus { get; set; } = string.Empty;

        public CourseResponseDTO? CourseResponse { get; set; }
        public List<AssessmentResponseDTO>? AssessmentResponses { get; set; }

    }
}
