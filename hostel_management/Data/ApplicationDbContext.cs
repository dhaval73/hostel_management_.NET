using hostel_management.Models;
using Microsoft.EntityFrameworkCore;

namespace hostel_management.Data
{
    public class ApplicationDbContext : DbContext
    {
       

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ContactModel> Contacts { get; set; }
        public DbSet<SignUpModel> Users { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SignUpModel>()
                .Ignore(u => u.ConfirmPassword);
            modelBuilder.Entity<SignUpModel>()
                .Property(p => p.Role)
                .HasDefaultValue("User");

            modelBuilder.Entity<RoomModel>()
               .HasIndex(r => new { r.HostelName, r.RoomNumber })
               .IsUnique();
        }

    }
}
