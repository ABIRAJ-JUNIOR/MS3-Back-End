using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class CourseCategory
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //Reference
        public ICollection<Course>? courses { get; set; }
    }
}
