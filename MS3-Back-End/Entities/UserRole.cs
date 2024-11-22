namespace MS3_Back_End.Entities
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        //Reference
        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}
