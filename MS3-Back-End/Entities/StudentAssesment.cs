using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS3_Back_End.Entities
{
    public class StudentAssesment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid AssesmentId { get; set; }
        public int MarksObtaines { get; set; }
        public string Grade { get; set; }
        public string FeedBack { get; set; }
        public DateTime DateEvaluted { get; set; }
        public StudentAssesmentStatus StudentAssesmentStatus { get; set; }

        //Reference
        public Student? student { get; set; }
        public Assesment? assesment { get; set; }

    }
    public enum StudentAssesmentStatus
    {
        Absent=1,
        Completed=2,
        Reviewed=3
    }
}
