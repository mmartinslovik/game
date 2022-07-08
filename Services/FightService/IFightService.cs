public interface IFightService
{
    Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request);
    Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request);
    Task<ServiceResponse<FightResultDTO>> Fight(FightRequestDTO request);
    Task<ServiceResponse<List<HighscoreDTO>>> GetHighscore();
}