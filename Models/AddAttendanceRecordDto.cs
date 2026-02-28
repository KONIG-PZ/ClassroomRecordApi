using System.ComponentModel.DataAnnotations;

namespace ClassroomRecordApi.Models
{
    public class AddAttendanceRecordDto
    {
        [Required] public required DateOnly Date { get; set; }
        [Required][MaxLength(20)] public required string Status { get; set; }
        [MaxLength(200)] public string? Remarks { get; set; }
        [MaxLength(20)] public string? SchoolYear { get; set; }
        [Range(1, 4)] public int? Quarter { get; set; }
        [Required] public required Guid StudentId { get; set; }
        public Guid? ClassroomId { get; set; }
    }
}