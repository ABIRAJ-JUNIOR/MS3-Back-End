﻿namespace MS3_Back_End.DTOs.ResponseDTOs.ContactUs
{
    public class ContactUsResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public DateOnly DateSubmited { get; set; }
        public bool IsRead { get; set; }

    }
}
