using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("/ping")]
        public IActionResult Ping() => Ok("pong");
    }
}
