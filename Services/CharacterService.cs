using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class CharacterService : ICharacterService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CharacterService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<GetCharacterDTO>>> GetCharacters() 
    {
        ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
        List<Character> dbCharacters = await _context.Characters.ToListAsync();
        serviceResponse.Data = (dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c))).ToList();
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDTO>> GetCharacter(int id)
    {
        ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>();
        Character dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterDTO>(dbCharacter);

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter) 
    {
        ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
        Character character = _mapper.Map<Character>(newCharacter);

        await _context.Characters.AddAsync(character);
        await _context.SaveChangesAsync();
        serviceResponse.Data = (_context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c))).ToList();

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
    {
        ServiceResponse<GetCharacterDTO> serviceResponse = new ServiceResponse<GetCharacterDTO>();
        try
        {
            Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            character.Name = updatedCharacter.Name;
            character.Class = updatedCharacter.Class;
            character.Defense = updatedCharacter.Defense;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Strength = updatedCharacter.Strength;

            _context.Characters.Update(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
        } catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
    {
        ServiceResponse<List<GetCharacterDTO>> serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
        try 
        {
            Character character = await _context.Characters.FirstAsync(c => c.Id == id);
            _context.Characters.Remove(character);

            await _context.SaveChangesAsync();

            serviceResponse.Data = (_context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c))).ToList();
        } catch (Exception ex) 
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}