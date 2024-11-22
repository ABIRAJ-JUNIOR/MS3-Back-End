using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class Feedbacks
    {
        [Key]
        public Guid Id { get; set; }
        public string FeedBackText { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime FeedBackDate { get; set; }

        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }

        //Reference
        public Student Student { get; set; } = new Student();
        public Course Course { get; set; } = new Course();

    }
}
