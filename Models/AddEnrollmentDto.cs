using System.ComponentModel.DataAnnotations;

namespace ClassroomRecordApi.Models
{
    public class AddEnrollmentInfoDto
    {
        [Required] public required DateOnly EnrollmentDate { get; set; }
        [Required][MaxLength(20)] public required string SchoolYear { get; set; }
        [Required][Range(7, 12)] public required int GradeLevel { get; set; }
        [MaxLength(100)] public string? Section { get; set; }
        [MaxLength(100)] public string? Track { get; set; }
        [MaxLength(100)] public string? Strand { get; set; }
        [MaxLength(50)] public string? Status { get; set; }
        [MaxLength(200)] public string? Remarks { get; set; }
        [Required] public required Guid StudentId { get; set; }
        public Guid? ClassroomId { get; set; }
    }
}