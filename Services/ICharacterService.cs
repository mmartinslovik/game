public interface ICharacterService
{
    List<Character> GetCharacters();
    Character GetCharacter(int id);
    List<Character> AddCharacter(Character newCharacter);
}