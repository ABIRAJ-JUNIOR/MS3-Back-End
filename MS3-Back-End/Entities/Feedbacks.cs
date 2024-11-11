using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class Feedbacks
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public string FeedBackText { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string FeedBackDate { get; set; } = string.Empty;

        //Reference
        public User? User { get; set; }
        public Course? Course { get; set; }

    }
}
