namespace MS3_Back_End.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime DateSent { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsRead{ get; set; }

        public Guid StudentId { get; set; }

        //Reference
        public Student? Student { get; set; }
    }
    public enum NotificationType
    {
        PaymentReminder = 1,
        SheduleUpdate = 2,
        CourseOffering = 3,
    }
}
