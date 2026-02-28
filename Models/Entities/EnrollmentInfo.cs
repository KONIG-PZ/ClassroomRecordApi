using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClassroomRecordApi.Models.Entities
{
    public class EnrollmentInfo
    {
        public Guid Id { get; set; }
        [Required] public required DateOnly EnrollmentDate { get; set; }
        [Required][MaxLength(20)] public required string SchoolYear { get; set; }
        [Required][Range(7, 12)] public required int GradeLevel { get; set; }
        [MaxLength(100)] public string? Section { get; set; }
        [MaxLength(100)] public string? Track { get; set; }
        [MaxLength(100)] public string? Strand { get; set; }
        [MaxLength(50)] public string? Status { get; set; }
        [MaxLength(200)] public string? Remarks { get; set; }

        public Guid StudentId { get; set; }
        [JsonIgnore] public StudentInfo? Student { get; set; }

        public Guid? ClassroomId { get; set; }
        [JsonIgnore] public ClassroomInfo? Classroom { get; set; }
    }
}