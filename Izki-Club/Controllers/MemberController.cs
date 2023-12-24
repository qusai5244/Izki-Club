using Izki_Club.Dtos.GeneralDtos;
using Izki_Club.Dtos.MemberDtos;
using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Izki_Club.Enums.Member.MemberTypeEnum;

namespace Izki_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;

        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewMemberDto>))]
        public async Task<IActionResult> CreateMember([FromForm] AddMemberDto input)
        {
            var response = await _memberService.CreateMember(input);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(response);
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PaginatedList<ViewMemberDto>>))]
        public async Task<IActionResult> GetMembers([FromQuery] ViewMembersByType input)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _memberService.GetMembers(input);

            return Ok(response);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewMemberDto>))]
        public async Task<IActionResult> GetMember([FromRoute]int Id)
        {
            var response = await _memberService.GetMember(Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(response);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewMemberDto>))]
        public async Task<IActionResult> UpdateMember([FromRoute] int Id, [FromForm] UpdateMemberDto input)
        {
            var response = await _memberService.UpdateMember(Id, input);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewMemberDto>))]
        public async Task<IActionResult> DeleteMember([FromRoute] int Id)
        {
            var response = await _memberService.DeleteMember(Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(response);
        }


    }
}
