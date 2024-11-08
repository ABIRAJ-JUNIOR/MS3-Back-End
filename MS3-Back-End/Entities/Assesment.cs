using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class Assesment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public AssesmentType AssesmentType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalMarks { get; set; }
        public int PassMarks { get; set; }
        public DateOnly CreatedDate { get; set; }
        public DateOnly UpdateDate { get; set; }

        //Reference
        public Course? Course { get; set; }
        public ICollection<StudentAssesment>? StudentAssesments { get; set; }
    }

    public enum AssesmentType
    {
        Quiz=1,
        Exam=2
    }
}
