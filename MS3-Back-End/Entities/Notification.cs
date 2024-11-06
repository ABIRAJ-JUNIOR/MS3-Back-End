namespace MS3_Back_End.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateOnly DateSent { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsRead{ get; set; }

        //Reference
        public Student? student { get; set; }
    }
    public enum NotificationType
    {
        PaymentReminder = 1,
        SheduleUpdate = 2,
        CourseOffering = 3,
    }
}
