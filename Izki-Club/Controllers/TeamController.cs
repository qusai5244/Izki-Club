using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Dtos.TeamDtos;
using Izki_Club.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Izki_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {

        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PaginatedList<ViewTeamDto>>))]
        public async Task<IActionResult> GetTeams([FromQuery] SearchAndPaginationDto input)
        {
            var response = await _teamService.GetTeams(input);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewTeamDto>))]
        public async Task<IActionResult> CreateTeam([FromForm] AddTeamDto input)
        {
            var response = await _teamService.CreateTeam(input);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(response);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewTeamDto>))]
        public async Task<IActionResult> GetTeam([FromRoute] int Id)
        {
            var response = await _teamService.GetTeam(Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(response);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewTeamDto>))]
        public async Task<IActionResult> UpdateTeam([FromRoute] int Id, [FromForm] UpdateTeamDto input)
        {
            var response = await _teamService.UpdateTeam(Id, input);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewTeamDto>))]
        public async Task<IActionResult> DeleteTeam([FromRoute] int Id)
        {
            var response = await _teamService.DeleteTeam(Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(response);
        }
    }
}
