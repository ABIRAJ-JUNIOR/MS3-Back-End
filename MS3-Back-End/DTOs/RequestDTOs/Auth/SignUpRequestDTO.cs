namespace MS3_Back_End.DTOs.RequestDTOs.Auth
{
    public class SignUpRequestDTO
    {
        public string Nic { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string? Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserRoleRequestDTOcs? UserRole { get; set; }

    }
}
