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
        public DbSet<GradeRecord> GradeRecords { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<EnrollmentInfo> Enrollments { get; set; }
        public DbSet<ParentGuardianInfo> ParentGuardians { get; set; }
        public DbSet<AnnouncementInfo> Announcements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnnouncementInfo>()
                .HasOne(a => a.Classroom)
                .WithMany(c => c.Announcements)
                .HasForeignKey(a => a.ClassroomId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AnnouncementInfo>()
                .HasOne(a => a.School)
                .WithMany(s => s.Announcements)
                .HasForeignKey(a => a.SchoolId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AnnouncementInfo>()
                .HasOne(a => a.Teacher)
                .WithMany(t => t.Announcements)
                .HasForeignKey(a => a.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ParentGuardianInfo>()
                .HasOne(p => p.Student)
                .WithMany(s => s.ParentGuardians)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<EnrollmentInfo>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EnrollmentInfo>()
                .HasOne(e => e.Classroom)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.ClassroomId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AttendanceRecord>()
                .HasOne(a => a.Student)
                .WithMany(s => s.AttendanceRecords)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AttendanceRecord>()
                .HasOne(a => a.Classroom)
                .WithMany(c => c.AttendanceRecords)
                .HasForeignKey(a => a.ClassroomId)
                .OnDelete(DeleteBehavior.SetNull);

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

            modelBuilder.Entity<GradeRecord>()
                .HasOne(g => g.Student)
                .WithMany(s => s.GradeRecords)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GradeRecord>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.GradeRecords)
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GradeRecord>()
                .HasOne(g => g.Classroom)
                .WithMany(c => c.GradeRecords)
                .HasForeignKey(g => g.ClassroomId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}