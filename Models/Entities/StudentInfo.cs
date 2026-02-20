using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ClassroomRecordApi.Models.Entities
{
    public class StudentInfo
    {
        public Guid Id { get; set; }
        [Required][MaxLength(100)]public required string FirstName { get; set; }
        [Required][MaxLength(100)]public required string LastName { get; set; }
        [Required][MaxLength(100)]public required string MiddleName { get; set; }
        [Required]public required DateOnly DateofBirth { get; set; }
        [MaxLength(10)]public string Gender { get; set; }
        [Required][EmailAddress]public string Email { get; set; }
        [Phone][MaxLength(20)]public string? PhoneNumber { get; set; }
        [MaxLength(200)]public string Address { get; set; }
        [Required][Range(7, 12)]public required int GradeLevel { get; set; }
        [MaxLength(100)]public string Section { get; set; }
        [MaxLength(200)]public string AdvicerName { get; set; }
        public Guid? ClassroomId { get; set; }
        [JsonIgnore] public ClassroomInfo? Classroom { get; set; }

    }
}
