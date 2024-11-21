namespace MS3_Back_End.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //Reference
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
