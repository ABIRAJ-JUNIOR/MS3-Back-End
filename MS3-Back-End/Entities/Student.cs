using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nic { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Gender { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

        //Reference
        public Address? Address { get; set; }

    }
}
