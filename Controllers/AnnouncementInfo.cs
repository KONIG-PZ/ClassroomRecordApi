using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClassroomRecordApi.Models.Entities
{
    public class AnnouncementInfo
    {
        public Guid Id { get; set; }
        [Required][MaxLength(200)] public required string Title { get; set; }
        [Required][MaxLength(2000)] public required string Content { get; set; }
        [Required] public required DateOnly DatePosted { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        [MaxLength(50)] public string? Category { get; set; }
        public bool IsSchoolWide { get; set; }

        public Guid? ClassroomId { get; set; }
        [JsonIgnore] public ClassroomInfo? Classroom { get; set; }

        public Guid? SchoolId { get; set; }
        [JsonIgnore] public SchoolInfo? School { get; set; }

        public Guid? TeacherId { get; set; }
        [JsonIgnore] public TeacherInfo? Teacher { get; set; }
    }
}