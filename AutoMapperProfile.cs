using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDTO>();
        CreateMap<AddCharacterDTO, Character>();         
    } 
}
