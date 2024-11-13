using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Student
{
    public class StudentUpdateDTO
    {
        public Guid Id { get; set; }
        public string? Nic { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; } = DateTime.MinValue;
        public Gender? Gender { get; set; }
        public string? Phone { get; set; } = string.Empty;
        public string? ImagePath { get; set; } = string.Empty;

    }
}
