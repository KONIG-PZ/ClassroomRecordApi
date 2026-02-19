using System.ComponentModel.DataAnnotations;

namespace ClassroomRecordApi.Models.Entities;

public class UserInfo
{
    public Guid Id { get; set; }
    [Required][MaxLength(50)] public string Username { get; set; } = "";
    [Required][MaxLength(256)] public string PasswordHash { get; set; } = "";
    [Required][MaxLength(20)] public string Role { get; set; } = "";
    [Required][MaxLength(100)][EmailAddress] public string Email { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public TeacherInfo? TeacherInfo { get; set; }
}