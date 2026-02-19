using System.ComponentModel.DataAnnotations;

namespace ClassroomRecordApi.Models
{
    public class UpdateTeacherDto
    {
        [Required][MaxLength(100)] public required string FirstName { get; set; }
        [Required][MaxLength(100)] public required string LastName { get; set; }
        [Required][EmailAddress][MaxLength(100)] public required string Email { get; set; }
        [Phone][MaxLength(20)] public string? PhoneNumber { get; set; }
        [MaxLength(100)] public string? Department { get; set; }
    }
}
