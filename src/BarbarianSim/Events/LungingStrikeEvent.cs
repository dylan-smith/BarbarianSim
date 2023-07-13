using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class LungingStrikeEvent : EventInfo
{
    public LungingStrikeEvent(double timestamp, EnemyState target) : base(timestamp) => Target = target;

    public EnemyState Target { get; init; }
    public DirectDamageEvent DirectDamageEvent { get; set; }
    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
    public AuraAppliedEvent WeaponCooldownAuraAppliedEvent { get; set; }
    public double BaseDamage { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        FuryGeneratedEvent = new FuryGeneratedEvent(Timestamp, LungingStrike.FURY_GENERATED);
        state.Events.Add(FuryGeneratedEvent);

        var weaponDamage = (LungingStrike.Weapon.MinDamage + LungingStrike.Weapon.MaxDamage) / 2.0;
        var skillMultiplier = LungingStrike.GetSkillMultiplier(state);
        BaseDamage = weaponDamage * skillMultiplier;

        DirectDamageEvent = new DirectDamageEvent(Timestamp, BaseDamage, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, LungingStrike.LUCKY_HIT_CHANCE, LungingStrike.Weapon.Expertise, Target);
        state.Events.Add(DirectDamageEvent);

        var weaponSpeed = 1 / LungingStrike.Weapon.AttacksPerSecond;
        weaponSpeed *= AttackSpeedCalculator.Calculate(state);
        WeaponCooldownAuraAppliedEvent = new AuraAppliedEvent(Timestamp, weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(WeaponCooldownAuraAppliedEvent);
    }
}
