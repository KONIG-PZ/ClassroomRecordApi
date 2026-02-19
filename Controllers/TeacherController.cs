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
        public IActionResult AddTeacher(AddTeacherDto addTeacherDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
        [HttpPut]
        public IActionResult UpdateTeacherDto (Guid id, UpdateTeacherDto updateTeacherDto)
        {
            var teacher = dbContext.Teachers.Find(id);
            if (teacher is null)
                {
                return NotFound("Teacher Not Found");
                }
            teacher.FirstName = updateTeacherDto.FirstName;
            teacher.LastName = updateTeacherDto.LastName;
            teacher.Email = updateTeacherDto.Email;
            teacher.PhoneNumber = updateTeacherDto.PhoneNumber;
            teacher.Department = updateTeacherDto.Department;

            dbContext.SaveChanges();
            return Ok(teacher);
        }

        //Delete teacher
        [HttpDelete ("({id:guid}")]
        public IActionResult DeleteTeacher(Guid id)
        {
            var teacher = dbContext.Teachers.Find(id);

            if (teacher is null)
                return NotFound();

            var user = dbContext.Users.Find(teacher.UserId);
            if (user is not null)

            dbContext.Users.Remove(user);
            dbContext.Teachers.Remove(teacher);
            dbContext.SaveChanges();

            return Ok();
        }

    }
}
