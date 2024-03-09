using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Dtos.MemberDtos;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Dtos.RefereeDtos;
using Izki_Club.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Izki_Club.Enums.Member.MemberTypeEnum;

namespace Izki_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefereeController : ControllerBase
    {
        private readonly IRefereeService _refereeService;

        public RefereeController(IRefereeService refereeService)
        {
            _refereeService = refereeService;

        }

        [HttpPost]
        public async Task<IActionResult> CreateMember([FromForm] AddRefereeDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _refereeService.CreatReferee(input);

            return Ok(response);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PaginatedList<ViewMemberDto>>))]
        public async Task<IActionResult> GetMembers([FromBody] SearchAndPaginationDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _refereeService.GetReferees(input);

            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetMember([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _refereeService.GetReferee(Id);

            return Ok(response);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateMember([FromRoute] int Id, [FromForm] UpdateRefereeDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _refereeService.UpdateReferee(Id, input);

            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMember([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _refereeService.DeleteReferee(Id);

            return Ok(response);
        }


    }
}
