namespace MS3_Back_End.DTOs.RequestDTOs.__Password__
{
    public class UpdatePasswordRequestDTO
    {
        public Guid Id { get; set; }
        public string oldPassword { get; set; } = string.Empty;
        public string newPassword { get; set; } = string.Empty;
    }
}
