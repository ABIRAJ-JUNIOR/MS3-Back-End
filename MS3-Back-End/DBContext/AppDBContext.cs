using Microsoft.EntityFrameworkCore;
using MS3_Back_End.Entities;

namespace MS3_Back_End.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }  
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSchedule> CourseSchedules { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Announcement> Announcements { get; set; }  
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Feedbacks> Feedbacks { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Assesment> Assesments { get; set; }
        public DbSet<StudentAssesment> StudentAssesments { get; set; }

    }
}
