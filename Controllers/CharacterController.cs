using Microsoft.AspNetCore.Mvc;

namespace dotnet_project1.Controllers;

[ApiController]
[Route("api/characters")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _characterService.GetCharacters());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) 
    {
        return Ok(await _characterService.GetCharacter(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddCharacter(AddCharacterDTO newCharacter)
    {
        return Ok(await _characterService.AddCharacter(newCharacter));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
    {
        ServiceResponse<GetCharacterDTO> serviceResponse = await _characterService.UpdateCharacter(updatedCharacter); 

        if (serviceResponse.Data == null)
        {
            return NotFound(serviceResponse);
        }

        return Ok(serviceResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacter(int id) 
    {
        ServiceResponse<List<GetCharacterDTO>> serviceResponse = await _characterService.DeleteCharacter(id);

        if (serviceResponse.Data == null) 
        {
            return NotFound(serviceResponse);
        }

        return Ok(serviceResponse);
    }
}