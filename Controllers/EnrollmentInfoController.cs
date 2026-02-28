using ClassroomRecordApi.Data;
using ClassroomRecordApi.Models;
using ClassroomRecordApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassroomRecordApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentInfoController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public EnrollmentInfoController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var allEnrollments = await dbContext.Enrollments.ToListAsync();
            return Ok(allEnrollments);
        }

        [HttpPost]
        public IActionResult AddEnrollment(AddEnrollmentInfoDto addEnrollmentInfoDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var enrollmentEntity = new EnrollmentInfo()
            {
                EnrollmentDate = addEnrollmentInfoDto.EnrollmentDate,
                SchoolYear = addEnrollmentInfoDto.SchoolYear,
                GradeLevel = addEnrollmentInfoDto.GradeLevel,
                Section = addEnrollmentInfoDto.Section,
                Track = addEnrollmentInfoDto.Track,
                Strand = addEnrollmentInfoDto.Strand,
                Status = addEnrollmentInfoDto.Status,
                Remarks = addEnrollmentInfoDto.Remarks,
                StudentId = addEnrollmentInfoDto.StudentId,
                ClassroomId = addEnrollmentInfoDto.ClassroomId
            };

            dbContext.Enrollments.Add(enrollmentEntity);
            dbContext.SaveChanges();
            return Ok(enrollmentEntity);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetEnrollmentById(Guid id)
        {
            var enrollment = dbContext.Enrollments.Find(id);
            if (enrollment is null) return NotFound("Enrollment Not Found");
            return Ok(enrollment);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateEnrollment(Guid id, UpdateEnrollmentInfoDto updateEnrollmentInfoDto)
        {
            var enrollment = dbContext.Enrollments.Find(id);
            if (enrollment is null) return NotFound("Enrollment Not Found");

            enrollment.EnrollmentDate = updateEnrollmentInfoDto.EnrollmentDate;
            enrollment.SchoolYear = updateEnrollmentInfoDto.SchoolYear;
            enrollment.GradeLevel = updateEnrollmentInfoDto.GradeLevel;
            enrollment.Section = updateEnrollmentInfoDto.Section;
            enrollment.Track = updateEnrollmentInfoDto.Track;
            enrollment.Strand = updateEnrollmentInfoDto.Strand;
            enrollment.Status = updateEnrollmentInfoDto.Status;
            enrollment.Remarks = updateEnrollmentInfoDto.Remarks;
            enrollment.StudentId = updateEnrollmentInfoDto.StudentId;
            enrollment.ClassroomId = updateEnrollmentInfoDto.ClassroomId;

            dbContext.SaveChanges();
            return Ok(enrollment);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEnrollment(Guid id)
        {
            var enrollment = dbContext.Enrollments.Find(id);
            if (enrollment is null) return NotFound("Enrollment Not Found");

            dbContext.Enrollments.Remove(enrollment);
            dbContext.SaveChanges();
            return Ok();
        }

    }
}