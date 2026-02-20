using ClassroomRecordApi.Data;
using ClassroomRecordApi.Models;
using ClassroomRecordApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassroomRecordApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public SchoolController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/school
        [HttpGet]
        public async Task<IActionResult> GetAllSchools()
        {
            var schools = await dbContext.Schools.ToListAsync();
            return Ok(schools);
        }

        // GET: api/school/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSchoolById(Guid id)
        {
            var school = await dbContext.Schools
                .Include(s => s.Classrooms)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (school is null)
                return NotFound("School not found.");

            return Ok(school);
        }

        // POST: api/school
        [HttpPost]
        public async Task<IActionResult> AddSchool([FromBody] AddSchoolDto addSchoolDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var school = new SchoolInfo
            {
                SchoolName = addSchoolDto.SchoolName,
                SchoolCode = addSchoolDto.SchoolCode,
                Address = addSchoolDto.Address,
                City = addSchoolDto.City,
                Province = addSchoolDto.Province,
                Region = addSchoolDto.Region,
                PhoneNumber = addSchoolDto.PhoneNumber,
                Email = addSchoolDto.Email,
                Website = addSchoolDto.Website,
                PrincipalName = addSchoolDto.PrincipalName
            };

            dbContext.Schools.Add(school);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchoolById), new { id = school.Id }, school);
        }

        // PUT: api/school/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSchool(Guid id, [FromBody] UpdateSchoolDto updateSchoolDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var school = await dbContext.Schools.FindAsync(id);
            if (school is null)
                return NotFound("School not found.");

            school.SchoolName = updateSchoolDto.SchoolName;
            school.SchoolCode = updateSchoolDto.SchoolCode;
            school.Address = updateSchoolDto.Address;
            school.City = updateSchoolDto.City;
            school.Province = updateSchoolDto.Province;
            school.Region = updateSchoolDto.Region;
            school.PhoneNumber = updateSchoolDto.PhoneNumber;
            school.Email = updateSchoolDto.Email;
            school.Website = updateSchoolDto.Website;
            school.PrincipalName = updateSchoolDto.PrincipalName;

            await dbContext.SaveChangesAsync();
            return Ok(school);
        }

        // DELETE: api/school/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSchool(Guid id)
        {
            var school = await dbContext.Schools.FindAsync(id);
            if (school is null)
                return NotFound("School not found.");

            dbContext.Schools.Remove(school);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}