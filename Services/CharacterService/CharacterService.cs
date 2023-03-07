using AutoMapper;
using DotnetRpg.Data;
using DotnetRpg.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace DotnetRpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            this.context = context;
            this.mapper = mapper;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await context.Characters.ToListAsync();
            response.Data = dbCharacters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList();
            return response;

        }


        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }


        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = mapper.Map<Character>(newCharacter);
            context.Characters.Add(character);
            await context.SaveChangesAsync();
            serviceResponse.Data = await context.Characters.
            Select(c => mapper.Map<GetCharacterDto>(c))
            .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteAllCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await context.Characters.FirstAsync(c => c.Id == id);
                context.Characters.Remove(character);
                await context.SaveChangesAsync();
                response.Data = context.Characters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList();
                response.Message = "Character Deleted";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }


        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = await context.Characters
                .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                // mapper.Map(updatedCharacter, character);
                character.Name = updatedCharacter.Name;
                character.Hitpoints = updatedCharacter.Hitpoints;
                character.Strength = updatedCharacter.Strength;
                character.Defence = updatedCharacter.Defence;
                character.Intelegence = updatedCharacter.Intelegence;
                character.Class = updatedCharacter.Class;

                await context.SaveChangesAsync();

                response.Data = mapper.Map<GetCharacterDto>(character);
                response.Message = "Character Updated";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }
    }
}