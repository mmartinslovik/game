using Microsoft.AspNetCore.Mvc;

namespace dotnet_project1.Controllers;

[ApiController]
[Route("api/characters")]
public class CharacterController : ControllerBase 
{
    private static Character mage = new Character();

    [HttpGet()]
    public IActionResult Get() 
    {
        return Ok(mage);
    }

}