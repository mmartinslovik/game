public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterDTO>>> GetCharacters(int userId);
    Task<ServiceResponse<GetCharacterDTO>> GetCharacter(int id);
    Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO addCharacterDTO);
    Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updateCharacter);

    Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id);
}