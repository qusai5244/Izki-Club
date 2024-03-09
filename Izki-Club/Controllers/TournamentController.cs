using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Dtos.TeamDtos;
using Izki_Club.Dtos.TournamentDtos;
using Izki_Club.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Izki_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {

        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTournament([FromBody] AddTournamentDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _tournamentService.CreateTournament(input);

            return Ok(response);
        }        
        
        [HttpGet]
        public async Task<IActionResult> GetTournament([FromBody] GetTournamentDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _tournamentService.GetTournaments(input);

            return Ok(response);
        }

    }
}
