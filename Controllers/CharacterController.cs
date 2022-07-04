using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

    [HttpGet()]
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
}