using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class Assessment
    {
        [Key]
        public Guid Id { get; set; }
        public AssessmentType AssessmentType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalMarks { get; set; }
        public int PassMarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsActive { get; set; }

        public Guid CourseId { get; set; }

        //Reference
        public Course? Course { get; set; }
        public ICollection<StudentAssessment>? StudentAssessments { get; set; }
    }

    public enum AssessmentType
    {
        Quiz = 1,
        Exam = 2
    }
}
