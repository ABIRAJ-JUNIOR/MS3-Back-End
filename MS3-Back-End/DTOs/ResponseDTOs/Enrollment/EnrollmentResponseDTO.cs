﻿using MS3_Back_End.DTOs.ResponseDTOs.Payment;
using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.Enrollment
{
    public class EnrollmentResponseDTO
    {
        public Guid Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public Guid StudentId { get; set; }
        public Guid CourseSheduleId { get; set; }

        public ICollection<PaymentResponseDTO>? PaymentResponse { get; set; }
    }
}
