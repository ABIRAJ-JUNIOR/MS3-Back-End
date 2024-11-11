using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class AuditLog
    {
        [Key]
        public Guid Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public DateTime ActionDate { get; set; }
        public string Details { get; set; } = string.Empty;

        public Guid AdminId { get; set; }

        //Reference
        public Admin? Admin { get; set; }
    }

}
