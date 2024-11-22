using MS3_Back_End.DTOs.ResponseDTOs.Address;
using MS3_Back_End.DTOs.ResponseDTOs.Enrollment;
using MS3_Back_End.DTOs.ResponseDTOs.StudentAssessment;
using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.ResponseDTOs.Student
{
    public class StudentResponseDTO
    {
        public Guid Id { get; set; }
        public string Nic { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string Gender { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public DateTime CteatedDate { get; set; } = DateTime.MinValue;
        public DateTime? UpdatedDate { get; set; } = DateTime.MinValue;
        public bool IsActive { get; set; } = true;

        public AddressResponseDTO? Address { get; set; }
        public ICollection<EnrollmentResponseDTO>? Enrollments {  get; set; } 
        public ICollection<StudentAssessmentResponseDTO>? StudentAssessments { get; set; }

    }
}
