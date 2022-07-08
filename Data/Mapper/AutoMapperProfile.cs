using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDTO>().ForMember(dto => dto.Skills, c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));
        CreateMap<AddCharacterDTO, Character>(); 
        CreateMap<AddWeaponDTO, Weapon>();   
        CreateMap<Weapon, GetWeaponDTO>();
        CreateMap<Skill, GetSkillDTO>();
        CreateMap<Character, HighscoreDTO>();
    } 
}
