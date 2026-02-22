using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClassroomRecordApi.Models.Entities
{
    public class SubjectInfo
    {
        public Guid Id { get; set; }

        [Required][MaxLength(100)] public required string SubjectName { get; set; }
        [MaxLength(20)] public string? SubjectCode { get; set; }
        [MaxLength(100)] public string? LearningArea { get; set; }
        [MaxLength(20)] public string? Semester { get; set; }
        [Range(1, 4)] public int? Quarter { get; set; }
        [Required][Range(7, 12)] public required int GradeLevel { get; set; }
        [MaxLength(100)] public string? Track { get; set; }
        [MaxLength(100)] public string? Strand { get; set; }

        public Guid? ClassroomId { get; set; }
        [JsonIgnore] public ClassroomInfo? Classroom { get; set; }
        public Guid? TeacherId { get; set; }
        [JsonIgnore] public TeacherInfo? Teacher { get; set; }
    }
}