using System.ComponentModel.DataAnnotations;

namespace ClassroomRecordApi.Models
{
    public class AddAnnouncementInfoDto
    {
        [Required][MaxLength(200)] public required string Title { get; set; }
        [Required][MaxLength(2000)] public required string Content { get; set; }
        [Required] public required DateOnly DatePosted { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        [MaxLength(50)] public string? Category { get; set; }
        public bool IsSchoolWide { get; set; }
        public Guid? ClassroomId { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid? TeacherId { get; set; }
    }
}