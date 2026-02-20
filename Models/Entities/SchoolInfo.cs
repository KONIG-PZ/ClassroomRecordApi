using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClassroomRecordApi.Models.Entities
{
    public class SchoolInfo
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string SchoolName { get; set; }

        [MaxLength(50)]
        public string? SchoolCode { get; set; }  // DepEd code

        [MaxLength(300)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? Province { get; set; }

        [MaxLength(100)]
        public string? Region { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(200)]
        public string? Website { get; set; }

        [MaxLength(100)]
        public string? PrincipalName { get; set; }

        // Nav: classrooms belonging to this school
        [JsonIgnore]
        public ICollection<ClassroomInfo> Classrooms { get; set; } = new List<ClassroomInfo>();
    }
}