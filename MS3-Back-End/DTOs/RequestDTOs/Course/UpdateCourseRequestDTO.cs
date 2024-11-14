using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Course
{
    public class UpdateCourseRequestDTO
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CourseName { get; set; } = string.Empty;
        public CourseLevel? Level { get; set; }
        public decimal? CourseFee { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? Prerequisites { get; set; } = string.Empty;
        public string? ImagePath { get; set; } = string.Empty;
    }
}
