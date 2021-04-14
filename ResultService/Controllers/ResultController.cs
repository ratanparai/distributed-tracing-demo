using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateway.Attendance;
using Microsoft.AspNetCore.Mvc;

namespace ResultService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResultController
        : ControllerBase
    {
        private readonly IAttendanceServiceClient _attendanceServiceClient;

        public ResultController(IAttendanceServiceClient attendanceServiceClient)
        {
            _attendanceServiceClient = attendanceServiceClient ?? throw new ArgumentNullException(nameof(attendanceServiceClient));
        }

        [HttpGet("{id}", Name = "GetResult")]
        public async Task<ActionResult<Result>> GetAsync(string id)
        {
            var attendance = await _attendanceServiceClient.GetAttendanceAsync(id);

            var result = new Result
            {
                Cgpa = "5.4",
            };

            result.UpdateHoldStatus(attendance);

            return Ok(result);

        }
    }

    public class Result
    {
        public bool OnHold { get; private set; }

        public string Cgpa { get; set; }

        public void UpdateHoldStatus(Attendance attendance)
        {
            var percentage = Convert.ToInt32(attendance.AttendancePercentage.Trim('%'));

            OnHold = percentage <= 70;
        }
    }
}
