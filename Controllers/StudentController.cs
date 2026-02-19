using ClassroomRecordApi.Data;
using ClassroomRecordApi.Models;
using ClassroomRecordApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using static System.Collections.Specialized.BitVector32;

namespace ClassroomRecordApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public StudentController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get All
        [HttpGet]
        public async Task<IActionResult> GetAllStudent()
        {
            var allStudent = await dbContext.Students.ToListAsync();
            return Ok(allStudent);
        }

        //Post
        [HttpPost]
        public IActionResult AddStudent(AddStudentDto addStudentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var studentEntity = new StudentInfo()
            {
                FirstName = addStudentDto.FirstName,
                LastName = addStudentDto.LastName,
                MiddleName = addStudentDto.MiddleName,
                DateofBirth = addStudentDto.DateofBirth,
                Gender = addStudentDto.Gender,
                Email = addStudentDto.Email,
                PhoneNumber = addStudentDto.PhoneNumber,
                Address = addStudentDto.Address,
                GradeLevel = addStudentDto.GradeLevel,
                Section = addStudentDto.Section,
                AdvicerName = addStudentDto.AdvicerName
            };
            dbContext.Students.Add(studentEntity);
            dbContext.SaveChanges();

            return Ok(studentEntity);
        }

        //Get Id
        [HttpGet("{id:guid}")]
        public IActionResult GetStudentById(Guid id)
        {
            try
            {
                var student = dbContext.Students.Find(id);

                if (student is null)
                {
                    return NotFound("Student Not Found");
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        //Put
        [HttpPut("{id:guid}")]
        public IActionResult UpdateStudent(Guid id, UpdateStudentDto updateStudentDto)
        {
            var student = dbContext.Students.Find(id);
            if (student is null)
            {
                return NotFound();
            }
                student.FirstName = updateStudentDto.FirstName;
                student.LastName = updateStudentDto.LastName;
                student.MiddleName = updateStudentDto.MiddleName;
                student.DateofBirth = updateStudentDto.DateofBirth;
                student.Gender = updateStudentDto.Gender;
                student.Email = updateStudentDto.Email;
                student.PhoneNumber = updateStudentDto.PhoneNumber;
                student.Address = updateStudentDto.Address;
                student.GradeLevel = updateStudentDto.GradeLevel;
                student.Section = updateStudentDto.Section;
                student.AdvicerName = updateStudentDto.AdvicerName;

                dbContext.SaveChanges();

                return Ok(student);
               
        }

        //Delete
        [HttpDelete ("{id:guid}")]
        public IActionResult DeleteStudent(Guid id)
        {
            var student = dbContext.Students.Find(id);

            if (student is null)
            {
                return NotFound();
            }

            dbContext.Students.Remove(student);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
