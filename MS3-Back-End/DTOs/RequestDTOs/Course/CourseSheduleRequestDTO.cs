﻿using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Course
{
    public class CourseScheduleRequestDTO
    {
        public Guid CourseId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public int Duration { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int MaxStudents { get; set; }
        public ScheduleStatus ScheduleStatus { get; set; }

    }
}
