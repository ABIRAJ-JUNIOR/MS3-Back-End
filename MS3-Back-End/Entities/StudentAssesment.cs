using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS3_Back_End.Entities
{
    public class StudentAssesment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AssesmentId { get; set; }
        public int MarksObtaines { get; set; }
        public string Grade { get; set; } = string.Empty;
        public string FeedBack { get; set; } = string.Empty;
        public DateTime DateEvaluted { get; set; }
        public StudentAssesmentStatus StudentAssesmentStatus { get; set; }

        public Guid StudentId { get; set; }

        //Reference
        public Student? Student { get; set; }
        public Assesment? Assesment { get; set; }

    }
    public enum StudentAssesmentStatus
    {
        Absent=1,
        Completed=2,
        Reviewed=3
    }
}
