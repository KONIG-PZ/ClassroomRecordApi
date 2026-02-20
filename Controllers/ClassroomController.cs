using ClassroomRecordApi.Data;
using ClassroomRecordApi.Models;
using ClassroomRecordApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassroomRecordApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ClassroomController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/classroom
        [HttpGet]
        public async Task<IActionResult> GetAllClassrooms()
        {
            var classrooms = await dbContext.Classrooms
                .Include(c => c.Adviser)
                .Include(c => c.School)
                .ToListAsync();
            return Ok(classrooms);
        }

        // GET: api/classroom/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetClassroomById(Guid id)
        {
            var classroom = await dbContext.Classrooms
                .Include(c => c.Adviser)
                .Include(c => c.School)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (classroom is null)
                return NotFound("Classroom not found.");

            return Ok(classroom);
        }

        // GET: api/classroom/grade/{gradeLevel}
        [HttpGet("grade/{gradeLevel:int}")]
        public async Task<IActionResult> GetClassroomsByGrade(int gradeLevel)
        {
            if (gradeLevel < 7 || gradeLevel > 12)
                return BadRequest("GradeLevel must be between 7 and 12.");

            var classrooms = await dbContext.Classrooms
                .Where(c => c.GradeLevel == gradeLevel)
                .Include(c => c.Adviser)
                .ToListAsync();

            return Ok(classrooms);
        }

        // GET: api/classroom/{id}/students
        [HttpGet("{id:guid}/students")]
        public async Task<IActionResult> GetStudentsInClassroom(Guid id)
        {
            var classroom = await dbContext.Classrooms
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (classroom is null)
                return NotFound("Classroom not found.");

            return Ok(classroom.Students);
        }

        // POST: api/classroom
        [HttpPost]
        public async Task<IActionResult> AddClassroom([FromBody] AddClassroomDto addClassroomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (addClassroomDto.AdviserTeacherId.HasValue)
            {
                var teacher = await dbContext.Teachers.FindAsync(addClassroomDto.AdviserTeacherId.Value);
                if (teacher is null)
                    return BadRequest("Adviser teacher not found.");
            }

            if (addClassroomDto.SchoolId.HasValue)
            {
                var school = await dbContext.Schools.FindAsync(addClassroomDto.SchoolId.Value);
                if (school is null)
                    return BadRequest("School not found.");
            }

            var classroom = new ClassroomInfo
            {
                ClassName = addClassroomDto.ClassName,
                GradeLevel = addClassroomDto.GradeLevel,
                Section = addClassroomDto.Section,
                SchoolYear = addClassroomDto.SchoolYear,
                Department = addClassroomDto.Department,
                Room = addClassroomDto.Room,
                AdviserTeacherId = addClassroomDto.AdviserTeacherId,
                SchoolId = addClassroomDto.SchoolId
            };

            dbContext.Classrooms.Add(classroom);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClassroomById), new { id = classroom.Id }, classroom);
        }

        // PUT: api/classroom/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateClassroom(Guid id, [FromBody] UpdateClassroomDto updateClassroomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var classroom = await dbContext.Classrooms.FindAsync(id);
            if (classroom is null)
                return NotFound("Classroom not found.");

            if (updateClassroomDto.AdviserTeacherId.HasValue)
            {
                var teacher = await dbContext.Teachers.FindAsync(updateClassroomDto.AdviserTeacherId.Value);
                if (teacher is null)
                    return BadRequest("Adviser teacher not found.");
            }

            if (updateClassroomDto.SchoolId.HasValue)
            {
                var school = await dbContext.Schools.FindAsync(updateClassroomDto.SchoolId.Value);
                if (school is null)
                    return BadRequest("School not found.");
            }

            classroom.ClassName = updateClassroomDto.ClassName;
            classroom.GradeLevel = updateClassroomDto.GradeLevel;
            classroom.Section = updateClassroomDto.Section;
            classroom.SchoolYear = updateClassroomDto.SchoolYear;
            classroom.Department = updateClassroomDto.Department;
            classroom.Room = updateClassroomDto.Room;
            classroom.AdviserTeacherId = updateClassroomDto.AdviserTeacherId;
            classroom.SchoolId = updateClassroomDto.SchoolId;

            await dbContext.SaveChangesAsync();
            return Ok(classroom);
        }

        // DELETE: api/classroom/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteClassroom(Guid id)
        {
            var classroom = await dbContext.Classrooms.FindAsync(id);
            if (classroom is null)
                return NotFound("Classroom not found.");

            dbContext.Classrooms.Remove(classroom);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}