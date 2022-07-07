public class GetCharacterDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = "Medivh";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Mage;
    public GetWeaponDTO Weapon { get; set; }
}
