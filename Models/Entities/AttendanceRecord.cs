using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClassroomRecordApi.Models.Entities
{
    public class AttendanceRecord
    {
        public Guid Id { get; set; }
        [Required] public required DateOnly Date { get; set; }
        [Required][MaxLength (20)] public required string Status { get; set; }
        [MaxLength(200)] public string? Remarks { get; set; }
        [MaxLength(20)] public string? SchoolYear { get; set; }
        [Range(1, 4)] public int? Quarter { get; set; }

        public Guid StudentId { get; set; }
        [JsonIgnore] public StudentInfo? Student { get; set; }

        public Guid? ClassroomId { get; set; }
        [JsonIgnore] public ClassroomInfo? Classroom { get; set; }
    }
}
