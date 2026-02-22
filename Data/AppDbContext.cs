using ClassroomRecordApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassroomRecordApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<StudentInfo> Students { get; set; }
        public DbSet<TeacherInfo> Teachers { get; set; }
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<ClassroomInfo> Classrooms { get; set; } 
        public DbSet<SchoolInfo> Schools { get; set; }
        public DbSet<SubjectInfo> Subjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<UserInfo>()
                .HasOne(u => u.TeacherInfo)
                .WithOne(t => t.UserInfo)
                .HasForeignKey<TeacherInfo>(t => t.UserId);


            modelBuilder.Entity<ClassroomInfo>()
                .HasOne(c => c.School)
                .WithMany(s => s.Classrooms)
                .HasForeignKey(c => c.SchoolId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<ClassroomInfo>()
                .HasOne(c => c.Adviser)
                .WithMany(t => t.AdvisedClassrooms)
                .HasForeignKey(c => c.AdviserTeacherId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<StudentInfo>()
                .HasOne(s => s.Classroom)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassroomId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SubjectInfo>()
                .HasOne(s => s.Classroom)
                .WithMany(c => c.Subjects)
                .HasForeignKey(s => s.ClassroomId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SubjectInfo>()
                .HasOne(s => s.Teacher)
                .WithMany(t => t.Subjects)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}