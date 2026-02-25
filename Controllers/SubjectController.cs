using ClassroomRecordApi.Data;
using ClassroomRecordApi.Models;
using ClassroomRecordApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassroomRecordApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public SubjectController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await dbContext.Subjects.ToListAsync();
            return Ok(subjects);
        }

        // POST
        [HttpPost]
        public IActionResult AddSubject([FromBody] AddSubjectDto addSubjectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subjectEntity = new SubjectInfo()
            {
                SubjectName = addSubjectDto.SubjectName,
                SubjectCode = addSubjectDto.SubjectCode,
                LearningArea = addSubjectDto.LearningArea,
                Semester = addSubjectDto.Semester,
                Quarter = addSubjectDto.Quarter,
                GradeLevel = addSubjectDto.GradeLevel,
                Track = addSubjectDto.Track,
                Strand = addSubjectDto.Strand,
                ClassroomId = addSubjectDto.ClassroomId,
                TeacherId = addSubjectDto.TeacherId
            };

            dbContext.Subjects.Add(subjectEntity);
            dbContext.SaveChanges();
            return Ok(subjectEntity);
        }

        // GET ID
        [HttpGet("{id:guid}")]
        public IActionResult GetSubjectById(Guid id)
        {
            var subject = dbContext.Subjects.Find(id);
            if (subject is null)
                return NotFound("Subject Not Found");
            return Ok(subject);
        }

        // PUT
        [HttpPut("{id:guid}")]
        public IActionResult UpdateSubject(Guid id, [FromBody] UpdateSubjectDto updateSubjectDto)
        {
            var subject = dbContext.Subjects.Find(id);
            if (subject is null)
                return NotFound("Subject Not Found");

            subject.SubjectName = updateSubjectDto.SubjectName;
            subject.SubjectCode = updateSubjectDto.SubjectCode;
            subject.LearningArea = updateSubjectDto.LearningArea;
            subject.Semester = updateSubjectDto.Semester;
            subject.Quarter = updateSubjectDto.Quarter;
            subject.GradeLevel = updateSubjectDto.GradeLevel;
            subject.Track = updateSubjectDto.Track;
            subject.Strand = updateSubjectDto.Strand;
            subject.ClassroomId = updateSubjectDto.ClassroomId;
            subject.TeacherId = updateSubjectDto.TeacherId;

            dbContext.SaveChanges();
            return Ok(subject);
        }

        // DELETE
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteSubject(Guid id)
        {
            var subject = dbContext.Subjects.Find(id);
            if (subject is null)
                return NotFound("Subject Not Found");

            dbContext.Subjects.Remove(subject);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}