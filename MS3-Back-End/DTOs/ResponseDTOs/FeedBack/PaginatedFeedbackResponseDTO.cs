using MS3_Back_End.DTOs.ResponseDTOs.Course;
using MS3_Back_End.DTOs.ResponseDTOs.Student;

namespace MS3_Back_End.DTOs.ResponseDTOs.FeedBack
{
    public class PaginatedFeedbackResponseDTO
    {
        public Guid Id { get; set; }
        public string FeedBackText { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime FeedBackDate { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }

        public StudentResponseDTO? Student { get; set; }
        public CourseResponseDTO? Course { get; set; }
    }
}
