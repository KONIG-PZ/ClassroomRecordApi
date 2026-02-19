using ClassroomRecordApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassroomRecordApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<StudentInfo> Students{get; set;}
        public DbSet<TeacherInfo> Teachers { get; set; }
        public DbSet<UserInfo> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>()
                .HasOne(u => u.TeacherInfo)
                .WithOne(t => t.UserInfo)
                .HasForeignKey<TeacherInfo>(t => t.UserId);
        }
    }
}
