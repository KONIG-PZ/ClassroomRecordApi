using ClassroomRecordApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClassroomRecordApi.Models
{
    public class AddTeacherDto
    {
        // User credentials
        [Required][MaxLength(50)] public required string Username { get; set; }
        [Required] public required string Password { get; set; }
        [Required][MaxLength(20)] public required string Role { get; set; } = "Teacher";

        // Teacher details
        [Required][MaxLength(100)] public required string FirstName { get; set; }
        [Required][MaxLength(100)] public required string LastName { get; set; }
        [Required][EmailAddress][MaxLength(100)] public required string Email { get; set; }
        [Phone][MaxLength(20)] public string? PhoneNumber { get; set; }
        [MaxLength(100)] public string? Department { get; set; }
    }
}
