using Izki_Club.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Izki_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_authService.GenerateToken("dd"));
        }

        [Authorize]
        [HttpGet("dd")]
        public IActionResult Gett()
        {
            return Ok("hi");
        }
    }
}
