using ClassroomRecordApi.Data;
using ClassroomRecordApi.Models;
using ClassroomRecordApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassroomRecordApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceRecordController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public AttendanceRecordController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttendanceRecords()
        {
            var allAttendanceRecords = await dbContext.AttendanceRecords.ToListAsync();
            return Ok(allAttendanceRecords);
        }

        [HttpPost]
        public IActionResult AddAttendanceRecord(AddAttendanceRecordDto addAttendanceRecordDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var attendanceRecordEntity = new AttendanceRecord()
            {
                Date = addAttendanceRecordDto.Date,
                Status = addAttendanceRecordDto.Status,
                Remarks = addAttendanceRecordDto.Remarks,
                SchoolYear = addAttendanceRecordDto.SchoolYear,
                Quarter = addAttendanceRecordDto.Quarter,
                StudentId = addAttendanceRecordDto.StudentId,
                ClassroomId = addAttendanceRecordDto.ClassroomId
            };

            dbContext.AttendanceRecords.Add(attendanceRecordEntity);
            dbContext.SaveChanges();
            return Ok(attendanceRecordEntity);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetAttendanceRecordById(Guid id)
        {
            var attendanceRecord = dbContext.AttendanceRecords.Find(id);
            if (attendanceRecord is null) return NotFound("Attendance Record Not Found");
            return Ok(attendanceRecord);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateAttendanceRecord(Guid id, UpdateAttendanceRecordDto updateAttendanceRecordDto)
        {
            var attendanceRecord = dbContext.AttendanceRecords.Find(id);
            if (attendanceRecord is null) return NotFound("Attendance Record Not Found");

            attendanceRecord.Date = updateAttendanceRecordDto.Date;
            attendanceRecord.Status = updateAttendanceRecordDto.Status;
            attendanceRecord.Remarks = updateAttendanceRecordDto.Remarks;
            attendanceRecord.SchoolYear = updateAttendanceRecordDto.SchoolYear;
            attendanceRecord.Quarter = updateAttendanceRecordDto.Quarter;
            attendanceRecord.StudentId = updateAttendanceRecordDto.StudentId;
            attendanceRecord.ClassroomId = updateAttendanceRecordDto.ClassroomId;

            dbContext.SaveChanges();
            return Ok(attendanceRecord);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteAttendanceRecord(Guid id)
        {
            var attendanceRecord = dbContext.AttendanceRecords.Find(id);
            if (attendanceRecord is null) return NotFound("Attendance Record Not Found");

            dbContext.AttendanceRecords.Remove(attendanceRecord);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}