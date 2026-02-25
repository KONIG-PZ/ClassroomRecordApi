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
    public class GradeRecordController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public GradeRecordController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get All

        [HttpGet]
        public async Task<IActionResult> GetAllGradeRecords()
        {
            var allGradeRecords = await dbContext.GradeRecords.ToListAsync();
            return Ok(allGradeRecords);
        }

        //Post

        [HttpPost]
        public IActionResult AddGradeRecord([FromBody] AddGradeRecordDto addGradeRecordDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var gradeRecordEntity = new GradeRecord()
            {
                Quarter = addGradeRecordDto.Quarter,
                Semester = addGradeRecordDto.Semester,
                InitialGrade = addGradeRecordDto.InitialGrade,
                TransmutedGrade = addGradeRecordDto.TransmutedGrade,
                Remarks = addGradeRecordDto.Remarks,
                SchoolYear = addGradeRecordDto.SchoolYear,
                StudentId = addGradeRecordDto.StudentId,
                SubjectId = addGradeRecordDto.SubjectId,
                ClassroomId = addGradeRecordDto.ClassroomId
            };

            dbContext.GradeRecords.Add(gradeRecordEntity);
            dbContext.SaveChanges();
            return Ok(gradeRecordEntity);
        }

        //Get Id

        [HttpGet("{id:guid}")]
        public IActionResult GetGradeRecordById(Guid id)
        {
            var gradeRecord = dbContext.GradeRecords.Find(id);
            if (gradeRecord is null) return NotFound("Grade Record Not Found");
            return Ok(gradeRecord);
        }

        //Put
        [HttpPut("{id:guid}")]
        public IActionResult UpdateGradeRecord(Guid id, [FromBody] UpdateGradeRecordDto updateGradeRecordDto)
        {
            var gradeRecord = dbContext.GradeRecords.Find(id);
            if (gradeRecord is null) return NotFound("Grade Record Not Found");

            gradeRecord.Quarter = updateGradeRecordDto.Quarter;
            gradeRecord.Semester = updateGradeRecordDto.Semester;
            gradeRecord.InitialGrade = updateGradeRecordDto.InitialGrade;
            gradeRecord.TransmutedGrade = updateGradeRecordDto.TransmutedGrade;
            gradeRecord.Remarks = updateGradeRecordDto.Remarks;
            gradeRecord.SchoolYear = updateGradeRecordDto.SchoolYear;
            gradeRecord.StudentId = updateGradeRecordDto.StudentId;
            gradeRecord.SubjectId = updateGradeRecordDto.SubjectId;
            gradeRecord.ClassroomId = updateGradeRecordDto.ClassroomId;

            dbContext.SaveChanges();
            return Ok(gradeRecord);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteGradeRecord(Guid id)
        {
            var gradeRecord = dbContext.GradeRecords.Find(id);
            if (gradeRecord is null) return NotFound("Grade Record Not Found");

            dbContext.GradeRecords.Remove(gradeRecord);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
