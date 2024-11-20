﻿using MS3_Back_End.DTOs.ResponseDTOs.Assessment;
using MS3_Back_End.DTOs.ResponseDTOs.CourseCategory;
using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.Course
{
    public class CourseResponseDTO
    {
        public Guid Id { get; set; }
        public Guid CourseCategoryId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public CourseLevel Level { get; set; }
        public decimal CourseFee { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Prerequisites { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ICollection<CourseSheduleResponseDTO>? Shedules { get; set; }
        public ICollection<FeedbacksResponceDTO>? Feedbacks { get; set; }
    }
}
