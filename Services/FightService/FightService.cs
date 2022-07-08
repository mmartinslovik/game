using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

public class FightService : IFightService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public FightService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request)
    {
        ServiceResponse<AttackResultDTO> serviceResponse = new ServiceResponse<AttackResultDTO>();
        try
        {
            Character attacker = await _context.Characters
                .Include(c => c.Weapon)
                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
            Character opponent = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

            if (attacker == null || opponent == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Characters not found";

                return serviceResponse;
            }

            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength + 1));
            damage -= (new Random().Next(opponent.Defense) + 1);

            if (damage > 0)
                opponent.HitPoints -= (int)damage;
            if (opponent.HitPoints <= 0)
                serviceResponse.Message = $"{opponent.Name} has been defeated by {attacker.Name}!";

            _context.Characters.Update(opponent);
            await _context.SaveChangesAsync();
            serviceResponse.Data = new AttackResultDTO
            {
                Attacker = attacker.Name,
                AttackerHP = attacker.HitPoints,
                Opponent = opponent.Name,
                OpponentHP = opponent.HitPoints,
                Damage = damage
            };
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request)
    {
        ServiceResponse<AttackResultDTO> serviceResponse = new ServiceResponse<AttackResultDTO>();
        try
        {
            Character attacker = await _context.Characters
                .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
            Character opponent = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

            CharacterSkill characterSkill = attacker.CharacterSkills
                .FirstOrDefault(cs => cs.Skill.Id == request.SkillId);
            
            if (characterSkill == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"{attacker.Name} does not know this skill.";

                return serviceResponse;
            }

            int damage = characterSkill.Skill.Damage + (new Random().Next(attacker.Intelligence + 1));
            damage -= (new Random().Next(opponent.Defense) + 1);

            if (damage > 0)
                opponent.HitPoints -= (int)damage;
            if (opponent.HitPoints <= 0)
                serviceResponse.Message = $"{opponent.Name} has been defeated by {attacker.Name}!";

            _context.Characters.Update(opponent);
            await _context.SaveChangesAsync();
            serviceResponse.Data = new AttackResultDTO
            {
                Attacker = attacker.Name,
                AttackerHP = attacker.HitPoints,
                Opponent = opponent.Name,
                OpponentHP = opponent.HitPoints,
                Damage = damage
            };
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}