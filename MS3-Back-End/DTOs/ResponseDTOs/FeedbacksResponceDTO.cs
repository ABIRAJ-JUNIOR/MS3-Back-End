namespace MS3_Back_End.DTOs.ResponseDTOs
{
    public class FeedbacksResponceDTO
    {
        public Guid Id { get; set; }
        public string FeedBackText { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime FeedBackDate { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}
