using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace AddressService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController
        : ControllerBase
    {
        [HttpGet("{id}", Name = "GetAddress")]
        public ActionResult<Address> Get(string id)
        {
            Thread.Sleep(8000);
            var address = new Address
            {
                StudentId = id,
                City = "Dhaka",
                Country = "Bangladesh"
            };

            return Ok(address);
        }
    }

    public class Address
    {
        public string StudentId { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
