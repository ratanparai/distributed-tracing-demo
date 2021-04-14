using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendanceController
        : ControllerBase
    {
        [HttpGet("{id}", Name = "GetAttendance")]
        public ActionResult<Attendance> Get(string id)
        {
            if (new Random().Next() % 2 == 0)
            {
                throw new InvalidOperationException("You are out of luck");
            }

            Thread.Sleep(5000);

            var attendance = new Attendance
            {
                StudentId = id,
                TotalClasses = 250,
                AttendedClasses = 211,
            };

            return Ok(attendance);
        }

    }

    public class Attendance
    {
        public string StudentId { get; set; }

        public int TotalClasses { get; set; }

        public int AttendedClasses { get; set; }

        public string AttendancePercentage => $"{(double)AttendedClasses * 100 / TotalClasses}%";
    }
}
