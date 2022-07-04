using AutoMapper;

public class CharacterService : ICharacterService
{
    private readonly IMapper _mapper;
    public CharacterService(IMapper mapper)
    {
        _mapper = mapper;
    }

    private static List<Character> characters = new List<Character>
    {
        new Character(),
        new Character{ Id = 1, Name = "Khadgar" }
    };
    
    public async Task<ServiceResponse<List<GetCharacterDTO>>> GetCharacters() 
    {
        ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
        serviceResponse.Data = (characters.Select(c => _mapper.Map<GetCharacterDTO>(c))).ToList();
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDTO>> GetCharacter(int id)
    {
        ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>();
        serviceResponse.Data = _mapper.Map<GetCharacterDTO>(characters.FirstOrDefault(c => c.Id == id));

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter) 
    {
        ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
        Character character = _mapper.Map<Character>(newCharacter);
        character.Id = characters.Max(c => c.Id) + 1;
        characters.Add(character);
        serviceResponse.Data = (characters.Select(c => _mapper.Map<GetCharacterDTO>(c))).ToList();

        return serviceResponse;
    }
}