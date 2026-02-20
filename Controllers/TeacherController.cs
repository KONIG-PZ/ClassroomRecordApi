using ClassroomRecordApi.Data;
using ClassroomRecordApi.Models;
using ClassroomRecordApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassroomRecordApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public TeacherController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get All
        [HttpGet]
        public async Task<IActionResult> GetAllTeacher()
        {
            var allTeacher = await dbContext.Teachers.ToArrayAsync();
            return Ok(allTeacher);
        }

        //Post
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AddTeacher(AddTeacherDto addTeacherDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == addTeacherDto.Username);
            if (existingUser is not null)
                return Conflict("Username is already taken.");

            var userEntity = new UserInfo()
            {
                Username = addTeacherDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(addTeacherDto.Password),
                Role = addTeacherDto.Role,
                Email = addTeacherDto.Email
            };

            dbContext.Users.Add(userEntity);
            dbContext.SaveChanges();

            var teacherEntity = new TeacherInfo()
            {
                UserId = userEntity.Id,
                FirstName = addTeacherDto.FirstName,
                LastName = addTeacherDto.LastName,
                Email = addTeacherDto.Email,
                PhoneNumber = addTeacherDto.PhoneNumber,
                Department = addTeacherDto.Department
            };

            dbContext.Teachers.Add(teacherEntity);
            dbContext.SaveChanges();

            return Ok(teacherEntity);
        }

        //Get by TeacherInfo.Id
        [HttpGet("{id:guid}")]
        public IActionResult GetTeacherById(Guid id)
        {
            var teacher = dbContext.Teachers.Find(id);
            if (teacher is null) return NotFound("Teacher Not Found");
            return Ok(teacher);
        }

        // Get by UserInfo.UserId
        [HttpGet("user/{userId:guid}")]
        public IActionResult GetTeacherByUserId(Guid userId)
        {
            var teacher = dbContext.Teachers.FirstOrDefault(t => t.UserId == userId);
            if (teacher is null) return NotFound("Teacher Not Found");
            return Ok(teacher);
        }

        //Put
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTeacher(Guid id, [FromBody] UpdateTeacherDto updateTeacherDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacher = await dbContext.Teachers.FindAsync(id);
            if (teacher is null)
                return NotFound("Teacher not found.");

            teacher.FirstName = updateTeacherDto.FirstName;
            teacher.LastName = updateTeacherDto.LastName;
            teacher.Email = updateTeacherDto.Email;
            teacher.PhoneNumber = updateTeacherDto.PhoneNumber;
            teacher.Department = updateTeacherDto.Department;

            await dbContext.SaveChangesAsync();
            return Ok(teacher);
        }
        //Delete teacher
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTeacher(Guid id)
        {
            var teacher = await dbContext.Teachers.FindAsync(id);
            if (teacher is null)
                return NotFound("Teacher not found.");

            var user = await dbContext.Users.FindAsync(teacher.UserId);

            dbContext.Teachers.Remove(teacher);
            if (user is not null)
                dbContext.Users.Remove(user);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
