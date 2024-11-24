using MS3_Back_End.DTOs.RequestDTOs.Address;
using MS3_Back_End.Entities;

namespace MS3_Back_End.DTOs.RequestDTOs.Student
{
    public class StudentRequestDTO
    {
        public string Nic { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public Gender Gender { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public AddressRequestDTO? Address { get; set; }
    }
}
