using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/characterskills")]
public class CharacterSkillController : ControllerBase
{
    public readonly ICharacterSkillService _characterSkillService;

    public CharacterSkillController(ICharacterSkillService characterSkillService)
    {
        _characterSkillService = characterSkillService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCharacterSkill(AddCharacterSkillDTO newCharacterSkill)
    {
        return Ok(await _characterSkillService.AddCharacterSkill(newCharacterSkill));
    }
}