using ClassroomRecordApi.Data;
using ClassroomRecordApi.Models;
using ClassroomRecordApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassroomRecordApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentGuardianInfoController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ParentGuardianInfoController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get All
        [HttpGet]
        public async Task<IActionResult> GetAllParentGuardians()
        {
            var allParentGuardians = await dbContext.ParentGuardians.ToListAsync();
            return Ok(allParentGuardians);
        }

        //Post
        [HttpPost]
        public IActionResult AddParentGuardian(AddParentGuardianInfoDto addParentGuardianInfoDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var parentGuardianEntity = new ParentGuardianInfo()
            {
                FirstName = addParentGuardianInfoDto.FirstName,
                LastName = addParentGuardianInfoDto.LastName,
                MiddleName = addParentGuardianInfoDto.MiddleName,
                Relationship = addParentGuardianInfoDto.Relationship,
                ContactNumber = addParentGuardianInfoDto.ContactNumber,
                Email = addParentGuardianInfoDto.Email,
                Address = addParentGuardianInfoDto.Address,
                Occupation = addParentGuardianInfoDto.Occupation,
                IsPrimary = addParentGuardianInfoDto.IsPrimary,
                StudentId = addParentGuardianInfoDto.StudentId
            };

            dbContext.ParentGuardians.Add(parentGuardianEntity);
            dbContext.SaveChanges();
            return Ok(parentGuardianEntity);
        }

        //Get Id
        [HttpGet("{id:guid}")]
        public IActionResult GetParentGuardianById(Guid id)
        {
            var parentGuardian = dbContext.ParentGuardians.Find(id);
            if (parentGuardian is null) return NotFound("Parent/Guardian Not Found");
            return Ok(parentGuardian);
        }

        //Put
        [HttpPut("{id:guid}")]
        public IActionResult UpdateParentGuardian(Guid id, UpdateParentGuardianInfoDto updateParentGuardianInfoDto)
        {
            var parentGuardian = dbContext.ParentGuardians.Find(id);
            if (parentGuardian is null) return NotFound("Parent/Guardian Not Found");

            parentGuardian.FirstName = updateParentGuardianInfoDto.FirstName;
            parentGuardian.LastName = updateParentGuardianInfoDto.LastName;
            parentGuardian.MiddleName = updateParentGuardianInfoDto.MiddleName;
            parentGuardian.Relationship = updateParentGuardianInfoDto.Relationship;
            parentGuardian.ContactNumber = updateParentGuardianInfoDto.ContactNumber;
            parentGuardian.Email = updateParentGuardianInfoDto.Email;
            parentGuardian.Address = updateParentGuardianInfoDto.Address;
            parentGuardian.Occupation = updateParentGuardianInfoDto.Occupation;
            parentGuardian.IsPrimary = updateParentGuardianInfoDto.IsPrimary;
            parentGuardian.StudentId = updateParentGuardianInfoDto.StudentId;

            dbContext.SaveChanges();
            return Ok(parentGuardian);
        }

        //Delete
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteParentGuardian(Guid id)
        {
            var parentGuardian = dbContext.ParentGuardians.Find(id);
            if (parentGuardian is null) return NotFound("Parent/Guardian Not Found");

            dbContext.ParentGuardians.Remove(parentGuardian);
            dbContext.SaveChanges();
            return Ok();
        }

    }
}