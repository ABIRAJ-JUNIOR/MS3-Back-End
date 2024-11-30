using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Student
{
    public class StudentFullUpdateDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public Gender Gender { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;

        public StudentAddressRequestDTO? Address { get; set; }
    }
}
