using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class WhirlwindSpinEvent : EventInfo
{
    // Rapidly attack surrounding enemies for 17%[x] damage (Fury Cost: 11, Lucky Hit: 20%)
    public WhirlwindSpinEvent(Whirlwind whirlwind,
                              AuraAppliedEventFactory auraAppliedEventFactory,
                              FurySpentEventFactory furySpentEventFactory,
                              DirectDamageEventFactory directDamageEventFactory,
                              WhirlwindRefreshEventFactory whirlwindRefreshEventFactory,
                              AttackSpeedCalculator attackSpeedCalculator,
                              double timestamp) : base(timestamp)
    {
        _whirlwind = whirlwind;
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _furySpentEventFactory = furySpentEventFactory;
        _directDamageEventFactory = directDamageEventFactory;
        _whirlwindRefreshEventFactory = whirlwindRefreshEventFactory;
        _attackSpeedCalculator = attackSpeedCalculator;
    }

    private readonly Whirlwind _whirlwind;
    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly FurySpentEventFactory _furySpentEventFactory;
    private readonly DirectDamageEventFactory _directDamageEventFactory;
    private readonly WhirlwindRefreshEventFactory _whirlwindRefreshEventFactory;
    private readonly AttackSpeedCalculator _attackSpeedCalculator;

    public AuraAppliedEvent WhirlwindingAuraAppliedEvent { get; set; }
    public IList<DirectDamageEvent> DirectDamageEvents { get; init; } = new List<DirectDamageEvent>();
    public IList<FuryGeneratedEvent> FuryGeneratedEvents { get; init; } = new List<FuryGeneratedEvent>();
    public FurySpentEvent FurySpentEvent { get; set; }
    public WhirlwindRefreshEvent WhirlwindRefreshEvent { get; set; }
    public AuraAppliedEvent WeaponCooldownAuraAppliedEvent { get; set; }
    public double BaseDamage { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        WhirlwindingAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, 0, Aura.Whirlwinding);
        state.Events.Add(WhirlwindingAuraAppliedEvent);

        FurySpentEvent = _furySpentEventFactory.Create(Timestamp, Whirlwind.FURY_COST, SkillType.Core);
        state.Events.Add(FurySpentEvent);

        var weaponDamage = (_whirlwind.Weapon.MinDamage + _whirlwind.Weapon.MaxDamage) / 2.0;
        var skillMultiplier = _whirlwind.GetSkillMultiplier(state);
        BaseDamage = weaponDamage * skillMultiplier;

        foreach (var enemy in state.Enemies)
        {
            var directDamageEvent = _directDamageEventFactory.Create(Timestamp, BaseDamage, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, Whirlwind.LUCKY_HIT_CHANCE, _whirlwind.Weapon.Expertise, enemy);
            DirectDamageEvents.Add(directDamageEvent);
            state.Events.Add(directDamageEvent);
        }

        var weaponSpeed = 1 / _whirlwind.Weapon.AttacksPerSecond;
        weaponSpeed *= _attackSpeedCalculator.Calculate(state);
        WeaponCooldownAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(WeaponCooldownAuraAppliedEvent);

        WhirlwindRefreshEvent = _whirlwindRefreshEventFactory.Create(Timestamp + weaponSpeed);
        state.Events.Add(WhirlwindRefreshEvent);
    }
}
