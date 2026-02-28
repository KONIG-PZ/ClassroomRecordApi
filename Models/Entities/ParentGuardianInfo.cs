using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClassroomRecordApi.Models.Entities
{
    public class ParentGuardianInfo
    {
        public Guid Id { get; set; }
        [Required][MaxLength(100)] public required string FirstName { get; set; }
        [Required][MaxLength(100)] public required string LastName { get; set; }
        [MaxLength(100)] public string? MiddleName { get; set; }
        [Required][MaxLength(50)] public required string Relationship { get; set; }
        [Phone][MaxLength(20)] public string? ContactNumber { get; set; }
        [EmailAddress][MaxLength(100)] public string? Email { get; set; }
        [MaxLength(200)] public string? Address { get; set; }
        [MaxLength(100)] public string? Occupation { get; set; }
        public bool IsPrimary { get; set; }

        public Guid StudentId { get; set; }
        [JsonIgnore] public StudentInfo? Student { get; set; }
    }
}