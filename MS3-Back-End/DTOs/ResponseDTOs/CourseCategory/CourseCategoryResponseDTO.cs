namespace MS3_Back_End.DTOs.ResponseDTOs.CourseCategory
{
    public class CourseCategoryResponseDTO
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
