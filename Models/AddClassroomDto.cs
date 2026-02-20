using System.ComponentModel.DataAnnotations;

namespace ClassroomRecordApi.Models
{
    public class AddClassroomDto
    {
        [Required][MaxLength(50)]public required string ClassName { get; set; }

        [Required][Range(7, 12)]public required int GradeLevel { get; set; }

        [Required][MaxLength(100)]public required string Section { get; set; }

        [Required][MaxLength(100)]public required string SchoolYear { get; set; }

        [MaxLength(100)]public string? Department { get; set; }

        [MaxLength(100)]public string? Room { get; set; }

        public Guid? AdviserTeacherId { get; set; }
        public Guid? SchoolId { get; set; }
    }
}