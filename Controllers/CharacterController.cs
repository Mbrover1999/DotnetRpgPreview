using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetRpg.Dtos.Character;
using DotnetRpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace DotnetRpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CharacterController : ControllerBase
    {
        public ICharacterService CharacterService { get; }
        public CharacterController(ICharacterService characterService)
        {
            this.CharacterService = characterService;

        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {

            return Ok(await CharacterService.GetAllCharacter());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {

            return Ok(await CharacterService.GetCharacterById(id));

        }

        [HttpPost]

        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {

            return Ok(await CharacterService.AddCharacter(newCharacter));

        }

        [HttpPut]

        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var response = await CharacterService.UpdateCharacter(updatedCharacter);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id)
        {

             var response = await CharacterService.DeleteAllCharacter(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);


        }


    }
}