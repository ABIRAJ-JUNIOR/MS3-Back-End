using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;

        public int? StudentId { get; set; }

        //Reference
        public Student? Student { get; set; }



    }
}
