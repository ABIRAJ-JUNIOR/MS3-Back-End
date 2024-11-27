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
        public string? AssessmentLink { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public AssessmentStatus Status { get; set; }
        public Guid CourseId { get; set; }

        //Reference
        public Course? Course { get; set; }
        public ICollection<StudentAssessment>? StudentAssessments { get; set; }
    }

    public enum AssessmentType
    {
        Quiz = 1,
        Exam = 2,
        Presentation = 3,
        Practical = 4,
        OnlineTest = 5,
        Midterm = 6,
        FinalExam = 7,
        MockTest = 8,
        LabAssessment = 9,
        OpenBookTest = 10,
    }

    public enum AssessmentStatus
    {
        NotStarted = 1,   
        InProgress = 2,
        Completed = 3,
        Closed = 4
    }
}
