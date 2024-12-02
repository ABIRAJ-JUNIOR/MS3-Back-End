using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.Course
{
    public class Top3CourseDTO
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public CourseLevel Level { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public int FeedBackCount { get; set; }
        public double FeedBackRate { get; set; }

    }
}
