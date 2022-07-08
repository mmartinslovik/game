using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/fight")]
public class FightController : ControllerBase
{
    private readonly IFightService _fightService;

    public FightController(IFightService fightService)
    {
        _fightService = fightService;
    }

    [HttpPost("weapon")]
    public async Task<IActionResult> WeaponAttack(WeaponAttackDTO request)
    {
        return Ok(await _fightService.WeaponAttack(request));
    }

    [HttpPost("skill")]
    public async Task<IActionResult> SkillAttack(SkillAttackDTO request)
    {
        return Ok(await _fightService.SkillAttack(request));
    }
}

