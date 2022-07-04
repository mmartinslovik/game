public class CharacterService : ICharacterService
{
    private static List<Character> characters = new List<Character>
    {
        new Character(),
        new Character{ Id = 1, Name = "Khadgar" }
    };
    
    public List<Character> GetCharacters() 
    {
        return characters;
    }

    public Character GetCharacter(int id)
    {
        return characters.FirstOrDefault(character => character.Id == id);
    }

    public List<Character> AddCharacter(Character newCharacter) 
    {
        characters.Add(newCharacter);
        return characters;
    }
}