using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsConfirmEmail { get; set; } = false;
        public string Password { get; set; } = string.Empty;

        //Reference
        public UserRole UserRole { get; set; } = new UserRole();

    }
}
