using Microsoft.AspNetCore.Mvc;

namespace apis.Controllers
{
    [ApiController]
    public class ServerController : ControllerBase
    {
        [HttpGet("ping")]
        public string Ping()
        {
            return "Server is alive";
        }
    }
}