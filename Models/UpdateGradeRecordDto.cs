using System.ComponentModel.DataAnnotations;

namespace ClassroomRecordApi.Models
{
    public class UpdateGradeRecordDto
    {
        [Range(1, 4)] public int? Quarter { get; set; }
        [MaxLength(20)] public string? Semester { get; set; }
        public double? InitialGrade { get; set; }
        public double? TransmutedGrade { get; set; }
        [MaxLength(50)] public string? Remarks { get; set; }
        [MaxLength(20)] public string? SchoolYear { get; set; }
        [Required] public required Guid StudentId { get; set; }
        [Required] public required Guid SubjectId { get; set; }
        public Guid? ClassroomId { get; set; }
    }
}
