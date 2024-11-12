namespace MS3_Back_End.DTOs.ResponseDTOs.Address
{
    public class AddressResponseDTO
    {
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public Guid StudentId { get; set; }
    }
}
