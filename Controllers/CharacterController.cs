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
    public IActionResult Get()
    {
        return Ok(_characterService.GetCharacters());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id) 
    {
        return Ok(_characterService.GetCharacter(id));
    }

    [HttpPost]
    public IActionResult AddCharacter(Character newCharacter)
    {
        return Ok(_characterService.AddCharacter(newCharacter));
    }
}