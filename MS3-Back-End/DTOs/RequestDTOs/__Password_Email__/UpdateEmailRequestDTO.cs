namespace MS3_Back_End.DTOs.RequestDTOs.Admin
{
    public class UpdateEmailRequestDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;

    }
}
