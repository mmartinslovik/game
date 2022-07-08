public interface IFightService
{
    Task<ServiceResponse<AttackResultDTO>> WeaponAttack(WeaponAttackDTO request);
    Task<ServiceResponse<AttackResultDTO>> SkillAttack(SkillAttackDTO request);
}