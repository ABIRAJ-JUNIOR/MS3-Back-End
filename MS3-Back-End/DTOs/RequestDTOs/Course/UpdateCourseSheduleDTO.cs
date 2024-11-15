﻿using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Course
{
    public class UpdateCourseSheduleDTO
    {
        public Guid Id { get; set; }
        public Guid CourseId { get;  set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public string Time { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int MaxStudents { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ScheduleStatus ScheduleStatus { get; set; }

    }
}
