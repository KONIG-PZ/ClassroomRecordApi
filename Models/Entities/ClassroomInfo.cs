using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClassroomRecordApi.Models.Entities
{
    public class ClassroomInfo
    {
        public Guid Id { get; set; }
        [Required][MaxLength(50)] public required string ClassName { get; set; }
        [Required][Range(7, 12)] public required int GradeLevel { get; set; }
        [Required][MaxLength(100)] public required string Section { get; set; }
        [Required][MaxLength(100)]public required string SchoolYear { get; set; }
        [MaxLength(100)]public string? Department { get; set; }
        [MaxLength(100)]public string? Room { get; set; }
        public Guid? AdviserTeacherId { get; set; }
        [JsonIgnore]public TeacherInfo? Adviser { get; set; }
        public Guid? SchoolId { get; set; }
        [JsonIgnore] public SchoolInfo? School { get; set; }
        [JsonIgnore] public ICollection<StudentInfo> Students { get; set; } = new List<StudentInfo>();
        [JsonIgnore] public ICollection<SubjectInfo> Subjects { get; set; } = new List<SubjectInfo>();
        [JsonIgnore] public ICollection<GradeRecord> GradeRecords { get; set; } = new List<GradeRecord>();
        [JsonIgnore] public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
        [JsonIgnore] public ICollection<EnrollmentInfo> Enrollments { get; set; } = new List<EnrollmentInfo>();
        [JsonIgnore] public ICollection<AnnouncementInfo> Announcements { get; set; } = new List<AnnouncementInfo>();
    }
}
