using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class CourseCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //Reference
        public ICollection<Course>? Courses { get; set; }
    }
}
