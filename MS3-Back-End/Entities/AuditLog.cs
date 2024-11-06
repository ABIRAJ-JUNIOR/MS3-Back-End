using System.ComponentModel.DataAnnotations;

namespace MS3_Back_End.Entities
{
    public class AuditLog
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AdminId { get; set; }
        public string Action { get; set; }
        public DateOnly ActionDate { get; set; }
        public string Details { get; set; }

        //Reference
        public Admin? admins { get; set; }
    }

}
