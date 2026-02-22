using System.ComponentModel.DataAnnotations;

namespace ClassroomRecordApi.Models
{
    public class UpdateSubjectDto
    {
        [Required][MaxLength(100)] public required string SubjectName { get; set; }
        [MaxLength(20)] public string? SubjectCode { get; set; }
        [MaxLength(100)] public string? LearningArea { get; set; }
        [MaxLength(20)] public string? Semester { get; set; }
        [Range(1, 4)] public int? Quarter { get; set; }
        [Required][Range(7, 12)] public required int GradeLevel { get; set; }
        [MaxLength(100)] public string? Track { get; set; }
        [MaxLength(100)] public string? Strand { get; set; }
        public Guid? ClassroomId { get; set; }
        public Guid? TeacherId { get; set; }
    }
}