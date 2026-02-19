using System.ComponentModel.DataAnnotations;

namespace ClassroomRecordApi.Models.Entities;

public class TeacherInfo
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [Required][MaxLength(100)] public string FirstName { get; set; } = "";
    [Required][MaxLength(100)] public string LastName { get; set; } = "";
    [Required][MaxLength(100)][EmailAddress] public string Email { get; set; } = "";
    [MaxLength(20)] public string? PhoneNumber { get; set; }
    [MaxLength(100)] public string? Department { get; set; }
    public UserInfo UserInfo { get; set; } = null!;
}
