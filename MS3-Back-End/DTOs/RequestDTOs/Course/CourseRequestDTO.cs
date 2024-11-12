namespace MS3_Back_End.DTOs.RequestDTOs.Course
{
    public class CourseRequestDTO
    {
        public Guid CourseCategoryId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int Level { get; set; }
        public decimal CourseFee { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Prerequisites { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;

    }
}
