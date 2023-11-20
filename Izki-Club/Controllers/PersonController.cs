using Izki_Club.Dtos.PlayerDtos;
using Izki_Club.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Izki_Club.Helpers.Enum;

namespace Izki_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResponse<PaginatedList<ViewPersonDto>>))]
        public async Task<IActionResult> GetPersons(string searchInput, PersonType personType , int page = 1, int pageSize = 10)
        {
            var response = await _personService.GetPersons(searchInput, personType, page, pageSize);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewPersonDto>))]
        public async Task<IActionResult> CreatePerson([FromForm] AddPersonDto input)
        {
            var response = await _personService.CreatePerson(input);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewPersonDto>))]
        public async Task<IActionResult> GetPerson(int id)
        {
            var response = await _personService.GetPerson(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewPersonDto>))]
        public async Task<IActionResult> UpdatePerson([FromForm] UpdatePersonDto input)
        {
            var response = await _personService.UpdatePerson(input);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("Delete/{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewPersonDto>))]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var response = await _personService.DeletePerson(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("UpdatePersonStatus/{id}")]
        [ProducesResponseType(200, Type = typeof(ApiResponse<ViewPersonDto>))]
        public async Task<IActionResult> UpdatePersonStatus(int id)
        {
            var response = await _personService.UpdatePersonStatus(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
