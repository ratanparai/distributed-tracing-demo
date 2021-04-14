using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace AccountInfoService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountInfoController
        : ControllerBase
    {
        [HttpGet("{id}", Name= "GetAccountInfo")]
        public ActionResult<AccountInfo> Get(string id)
        {
            Thread.Sleep(1000);

            var info = new AccountInfo
            {
                Id = id,
                FirstName = "Ratan",
                LastName = "Parai",
            };

            return Ok(info);
        }
    }

    public class AccountInfo
    {
        public string Id { get; set; }

        public string FirstName { get; set;  }

        public string LastName { get; set; }

        public string DisplayName => $"{Id}: {FirstName} {LastName}";

    }
}
