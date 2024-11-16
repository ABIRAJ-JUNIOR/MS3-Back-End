using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Course
{
    public class CourseRequestDTO
    {
        public Guid CourseCategoryId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public CourseLevel Level { get; set; }
        public decimal CourseFee { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Prerequisites { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }

    }
}
