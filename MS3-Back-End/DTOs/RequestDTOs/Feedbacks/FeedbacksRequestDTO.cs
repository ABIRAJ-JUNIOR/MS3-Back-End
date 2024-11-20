namespace MS3_Back_End.DTOs.RequestDTOs.Feedbacks
{
    public class FeedbacksRequestDTO
    {
        public string FeedBackText { get; set; } = string.Empty;
        public int Rating { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}
