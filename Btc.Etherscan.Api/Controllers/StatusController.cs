using Microsoft.AspNetCore.Mvc;

namespace Btc.Etherscan.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Status: up.";
        }
    }
}
