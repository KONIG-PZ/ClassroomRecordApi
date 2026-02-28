using ClassroomRecordApi.Data;
using ClassroomRecordApi.Models;
using ClassroomRecordApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassroomRecordApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementInfoController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public AnnouncementInfoController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            var allAnnouncements = await dbContext.Announcements.ToListAsync();
            return Ok(allAnnouncements);
        }

        [HttpPost]
        public IActionResult AddAnnouncement(AddAnnouncementInfoDto addAnnouncementInfoDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var announcementEntity = new AnnouncementInfo()
            {
                Title = addAnnouncementInfoDto.Title,
                Content = addAnnouncementInfoDto.Content,
                DatePosted = addAnnouncementInfoDto.DatePosted,
                ExpiryDate = addAnnouncementInfoDto.ExpiryDate,
                Category = addAnnouncementInfoDto.Category,
                IsSchoolWide = addAnnouncementInfoDto.IsSchoolWide,
                ClassroomId = addAnnouncementInfoDto.ClassroomId,
                SchoolId = addAnnouncementInfoDto.SchoolId,
                TeacherId = addAnnouncementInfoDto.TeacherId
            };

            dbContext.Announcements.Add(announcementEntity);
            dbContext.SaveChanges();
            return Ok(announcementEntity);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetAnnouncementById(Guid id)
        {
            var announcement = dbContext.Announcements.Find(id);
            if (announcement is null) return NotFound("Announcement Not Found");
            return Ok(announcement);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateAnnouncement(Guid id, UpdateAnnouncementInfoDto updateAnnouncementInfoDto)
        {
            var announcement = dbContext.Announcements.Find(id);
            if (announcement is null) return NotFound("Announcement Not Found");

            announcement.Title = updateAnnouncementInfoDto.Title;
            announcement.Content = updateAnnouncementInfoDto.Content;
            announcement.DatePosted = updateAnnouncementInfoDto.DatePosted;
            announcement.ExpiryDate = updateAnnouncementInfoDto.ExpiryDate;
            announcement.Category = updateAnnouncementInfoDto.Category;
            announcement.IsSchoolWide = updateAnnouncementInfoDto.IsSchoolWide;
            announcement.ClassroomId = updateAnnouncementInfoDto.ClassroomId;
            announcement.SchoolId = updateAnnouncementInfoDto.SchoolId;
            announcement.TeacherId = updateAnnouncementInfoDto.TeacherId;

            dbContext.SaveChanges();
            return Ok(announcement);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteAnnouncement(Guid id)
        {
            var announcement = dbContext.Announcements.Find(id);
            if (announcement is null) return NotFound("Announcement Not Found");

            dbContext.Announcements.Remove(announcement);
            dbContext.SaveChanges();
            return Ok();
        }

    }
}