using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Izki_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from TeamController");
        }
    }
}
