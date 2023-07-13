using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class LungingStrikeEvent : EventInfo
{
    public LungingStrikeEvent(FuryGeneratedEventFactory furyGeneratedEventFactory,
                              LungingStrike lungingStrike,
                              DirectDamageEventFactory directDamageEventFactory,
                              AttackSpeedCalculator attackSpeedCalculator,
                              AuraAppliedEventFactory auraAppliedEventFactory,
                              double timestamp,
                              EnemyState target) : base(timestamp)
    {
        _furyGeneratedEventFactory = furyGeneratedEventFactory;
        _lungingStrike = lungingStrike;
        _directDamageEventFactory = directDamageEventFactory;
        _attackSpeedCalculator = attackSpeedCalculator;
        _auraAppliedEventFactory = auraAppliedEventFactory;
        Target = target;
    }

    private readonly FuryGeneratedEventFactory _furyGeneratedEventFactory;
    private readonly LungingStrike _lungingStrike;
    private readonly DirectDamageEventFactory _directDamageEventFactory;
    private readonly AttackSpeedCalculator _attackSpeedCalculator;
    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    public EnemyState Target { get; init; }
    public DirectDamageEvent DirectDamageEvent { get; set; }
    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
    public AuraAppliedEvent WeaponCooldownAuraAppliedEvent { get; set; }
    public double BaseDamage { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        FuryGeneratedEvent = _furyGeneratedEventFactory.Create(Timestamp, LungingStrike.FURY_GENERATED);
        state.Events.Add(FuryGeneratedEvent);

        var weaponDamage = (_lungingStrike.Weapon.MinDamage + _lungingStrike.Weapon.MaxDamage) / 2.0;
        var skillMultiplier = _lungingStrike.GetSkillMultiplier(state);
        BaseDamage = weaponDamage * skillMultiplier;

        DirectDamageEvent = _directDamageEventFactory.Create(Timestamp, BaseDamage, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, LungingStrike.LUCKY_HIT_CHANCE, _lungingStrike.Weapon.Expertise, Target);
        state.Events.Add(DirectDamageEvent);

        var weaponSpeed = 1 / _lungingStrike.Weapon.AttacksPerSecond;
        weaponSpeed *= _attackSpeedCalculator.Calculate(state);
        WeaponCooldownAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(WeaponCooldownAuraAppliedEvent);
    }
}
