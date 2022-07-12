using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Authorize(Roles = "Player, Admin")]
[ApiController]
[Route("api/characters")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    // [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
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