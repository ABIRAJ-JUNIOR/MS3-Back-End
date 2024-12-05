namespace MS3_Back_End.DTOs.RequestDTOs.Admin
{
    public class AdminProfileUpdateDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string? CurrentPassword {  get; set; } = string.Empty;
        public string? NewPassword {  get; set; } = string.Empty;
    }
}
