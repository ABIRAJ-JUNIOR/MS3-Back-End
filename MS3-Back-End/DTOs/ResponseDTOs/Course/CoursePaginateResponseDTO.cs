using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.FeedBack;

namespace MS3_Back_End.DTOs.ResponseDTOs.Course
{
    public class CoursePaginateResponseDTO
    {
        public Guid Id { get; set; }
        public Guid CourseCategoryId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public decimal CourseFee { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Prerequisites { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int FeedBackRate {  get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ICollection<CourseScheduleResponseDTO>? Schedules { get; set; }
        public ICollection<FeedbacksResponceDTO>? Feedbacks { get; set; }
        public ICollection<AssessmentResponseDTO>? AssessmentResponse { get; set; }
    }
}
