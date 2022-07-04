using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_project1.Controllers;

[ApiController]
[Route("api/characters")]
public class CharacterController : ControllerBase
{
    private static Character mage = new Character();

    private static List<Character> characters = new List<Character>
    {
        new Character(),
        new Character{ Id = 1, Name = "Khadgar" }
    };

    [HttpGet()]
    public IActionResult Get()
    {
        return Ok(characters);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id) 
    {
        return Ok(characters.FirstOrDefault(character => character.Id == id));
    }

    [HttpPost]
    public IActionResult AddCharacter(Character newCharacter)
    {
        characters.Add(newCharacter);
        return Ok(characters);
    }
}