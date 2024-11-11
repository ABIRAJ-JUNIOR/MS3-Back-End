﻿using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class StudentAssessment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AssesmentId { get; set; }
        public int MarksObtaines { get; set; }
        public string Grade { get; set; } = string.Empty;
        public string FeedBack { get; set; } = string.Empty;
        public DateTime DateEvaluted { get; set; }
        public StudentAssessmentStatus StudentAssessmentStatus { get; set; }

        public Guid StudentId { get; set; }

        //Reference
        public Student? Student { get; set; }
        public Assessment? Assessment { get; set; }

    }
    public enum StudentAssessmentStatus
    {
        Absent = 1,
        Completed = 2,
        Reviewed = 3
    }
}