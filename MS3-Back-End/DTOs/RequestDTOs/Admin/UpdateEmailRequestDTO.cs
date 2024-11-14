namespace MS3_Back_End.DTOs.RequestDTOs.Admin
{
    public class UpdateEmailRequestDTO
    {
        public Guid StudentId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
