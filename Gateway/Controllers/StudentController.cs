using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Gateway.AccountInfo;
using Gateway.AddressInfo;
using Gateway.ResultService;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController
        : ControllerBase
    {
        private readonly IAddressServiceClient _addressServiceClient;
        private readonly IAccountInfoClient _accountInfoClient;
        private readonly IResultServiceClient _resultServiceClient;

        public StudentController(
            IAddressServiceClient addressServiceClient, 
            IAccountInfoClient accountInfoClient,
            IResultServiceClient resultServiceClient)
        {
            _addressServiceClient = addressServiceClient ?? throw new ArgumentNullException(nameof(addressServiceClient));
            _accountInfoClient = accountInfoClient ?? throw new ArgumentNullException(nameof(accountInfoClient));
            _resultServiceClient = resultServiceClient ?? throw new ArgumentNullException(nameof(resultServiceClient));
        }

        [HttpGet("{id}", Name = "GetStudent")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            //var addressTask = _addressServiceClient.GetAddressAsync(id);
            //var infoTask = _accountInfoClient.GetAccountInfoAsync(id);
            //var resultTask = _resultServiceClient.GetResultAsync(id);

            //var address = await addressTask;
            //var info = await infoTask;
            //var result = await resultTask;

            var address = await _addressServiceClient.GetAddressAsync(id);
            var info = await _accountInfoClient.GetAccountInfoAsync(id);
            var result = await _resultServiceClient.GetResultAsync(id);

            var student = new Student
            {
                Id = address.StudentId,
                Name = info.DisplayName,
                City = address.City,
                Cgpa = result.Cgpa,
                IsResultOnHold = result.OnHold,
            };

            return Ok(student);
        }
    }

    public class Student
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Cgpa { get; set; }

        public bool IsResultOnHold { get; set; }
    }
}
