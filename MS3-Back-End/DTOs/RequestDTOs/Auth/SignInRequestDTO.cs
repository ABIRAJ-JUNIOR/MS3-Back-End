namespace MS3_Back_End.DTOs.RequestDTOs.Auth
{
    public class SignInRequestDTO
    {
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
