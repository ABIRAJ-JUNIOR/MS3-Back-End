namespace MS3_Back_End.DTOs.ResponseDTOs.Admin
{
    public class AdminResponseDTO
    {
        public Guid Id { get; set; }
        public string Nic { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? ImagePath { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
