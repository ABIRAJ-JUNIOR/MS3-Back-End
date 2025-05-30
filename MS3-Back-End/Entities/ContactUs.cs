﻿using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class ContactUs
    {
        [Key]
        public Guid Id{ get; set; }
        public string Name{ get; set; } = string.Empty;
        public string Email{ get; set; } = string.Empty;
        public string Message{ get; set; } = string.Empty;
        public string Response{ get; set; } = string.Empty;
        public DateTime DateSubmited { get; set; }
        public bool IsRead{ get; set; }
    }
}
