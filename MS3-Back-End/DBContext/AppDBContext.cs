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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(a => a.address)
                .WithOne(s => s.Student)
                .HasForeignKey<Address>(sId => sId.StudentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
