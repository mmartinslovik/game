using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

public class CharacterSkillService : ICharacterSkillService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CharacterSkillService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill(AddCharacterSkillDTO newCharacterSkill)
    {
        ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>();
        try
        {
            Character character = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId &&
                c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (character == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found.";

                return serviceResponse;
            }

            Skill skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);

            if (skill == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Skill not found.";

                return serviceResponse;
            }

            CharacterSkill characterSkill = new CharacterSkill
            {
                Character = character,
                Skill = skill
            };
            await _context.CharacterSkills.AddAsync(characterSkill);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}