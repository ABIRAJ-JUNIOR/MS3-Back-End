using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode {  get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public Guid StudentId { get; set; }

        //Reference
        public Student Student { get; set; } = new Student();

    }
}
