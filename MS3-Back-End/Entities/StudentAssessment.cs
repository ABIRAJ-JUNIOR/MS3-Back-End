using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class StudentAssessment
    {
        [Key]
        public Guid Id { get; set; }
        public int? MarksObtaines { get; set; }
        public Grade? Grade { get; set; }
        public string? FeedBack { get; set; } = string.Empty;
        public DateTime DateSubmitted { get; set; }
        public DateTime? DateEvaluated { get; set; }
        public StudentAssessmentStatus StudentAssessmentStatus { get; set; }

        public Guid StudentId { get; set; }
        public Guid AssessmentId { get; set; }

        //Reference
        public Student? Student { get; set; }
        public Assessment? Assessment { get; set; }

    }
    public enum StudentAssessmentStatus
    {
        Completed = 1,
        Reviewed = 2
    }

    public enum Grade
    {
        Pass = 1,
        Fail = 2
    }
}
