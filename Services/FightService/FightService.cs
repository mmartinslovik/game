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

            int damage = DoWeaponAttack(attacker, opponent);
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

            if (attacker == null || opponent == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Characters not found";

                return serviceResponse;
            }

            if (characterSkill == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"{attacker.Name} does not know this skill.";

                return serviceResponse;
            }

            int damage = DoSkillAttack(attacker, opponent, characterSkill);
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

    public async Task<ServiceResponse<FightResultDTO>> Fight(FightRequestDTO request)
    {
        ServiceResponse<FightResultDTO> serviceResponse = new ServiceResponse<FightResultDTO>()
        {
            Data = new FightResultDTO()
        };
        try
        {
            List<Character> characters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill)
                .Where(c => request.CharacterIds.Contains(c.Id)).ToListAsync();

            bool defeated = false;
            while (!defeated)
            {
                foreach (Character attacker in characters)
                {
                    List<Character> opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                    Character opponent = opponents[new Random().Next(opponents.Count)];
                    int damage = 0;
                    string attackUsed = string.Empty;

                    bool useWeapon = new Random().Next(2) == 0;
                    if (useWeapon)
                    {
                        attackUsed = attacker.Weapon.Name;
                        damage = DoWeaponAttack(attacker, opponent);
                    }
                    else
                    {
                        int randomSkill = new Random().Next(attacker.CharacterSkills.Count);
                        attackUsed = attacker.CharacterSkills[randomSkill].Skill.Name;
                        damage = DoSkillAttack(attacker, opponent, attacker.CharacterSkills[randomSkill]);
                    }
                    serviceResponse.Data.Log.Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage.");

                    if (opponent.HitPoints <= 0)
                    {
                        defeated = true;
                        attacker.Victories++;
                        opponent.Defeats++;
                        serviceResponse.Data.Log.Add($"{opponent.Name} has been defeated!");
                        serviceResponse.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left!");

                        break;
                    }
                }
            }
            
            characters.ForEach(c =>
                    {
                        c.Fights++;
                        c.HitPoints = 100;
                    });
            _context.Characters.UpdateRange(characters);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    private static int DoWeaponAttack(Character attacker, Character opponent)
    {
        int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength + 1));
        damage -= (new Random().Next(opponent.Defense) + 1);
        if (damage > 0)
            opponent.HitPoints -= (int)damage;

        return damage;
    }

    private static int DoSkillAttack(Character attacker, Character opponent, CharacterSkill characterSkill)
    {
        int damage = characterSkill.Skill.Damage + (new Random().Next(attacker.Intelligence + 1));
        damage -= (new Random().Next(opponent.Defense) + 1);

        if (damage > 0)
            opponent.HitPoints -= (int)damage;

        return damage;
    }

    public async Task<ServiceResponse<List<HighscoreDTO>>> GetHighscore()
    {
        List<Character> characters = await _context.Characters
            .Where(c => c.Fights > 0)
            .OrderByDescending(c => c.Victories)
            .ThenBy(c => c.Defeats)
            .ToListAsync();
            
        ServiceResponse<List<HighscoreDTO>> serviceResponse = new ServiceResponse<List<HighscoreDTO>>
        {
            Data = characters.Select(c => _mapper.Map<HighscoreDTO>(c)).ToList()
        };

        return serviceResponse;
    }
}