public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterDTO>>> GetCharacters();
    Task<ServiceResponse<GetCharacterDTO>> GetCharacter(int id);
    Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO addCharacterDTO);
}