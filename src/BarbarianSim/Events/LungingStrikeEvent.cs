using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class LungingStrikeEvent : EventInfo
{
    public LungingStrikeEvent(FuryGeneratedEventFactory furyGeneratedEventFactory,
                              DirectDamageEventFactory directDamageEventFactory,
                              AttackSpeedCalculator attackSpeedCalculator,
                              AuraAppliedEventFactory auraAppliedEventFactory,
                              double timestamp,
                              EnemyState target) : base(timestamp)
    {
        _furyGeneratedEventFactory = furyGeneratedEventFactory;
        _directDamageEventFactory = directDamageEventFactory;
        _attackSpeedCalculator = attackSpeedCalculator;
        _auraAppliedEventFactory = auraAppliedEventFactory;
        Target = target;
    }

    private readonly FuryGeneratedEventFactory _furyGeneratedEventFactory;
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

        var weaponDamage = (state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike].MinDamage + state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike].MaxDamage) / 2.0;
        var skillMultiplier = GetSkillMultiplier(state);
        BaseDamage = weaponDamage * skillMultiplier;

        DirectDamageEvent = _directDamageEventFactory.Create(Timestamp, BaseDamage, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, LungingStrike.LUCKY_HIT_CHANCE, state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike].Expertise, Target);
        state.Events.Add(DirectDamageEvent);

        var weaponSpeed = 1 / state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike].AttacksPerSecond;
        weaponSpeed *= _attackSpeedCalculator.Calculate(state);
        WeaponCooldownAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(WeaponCooldownAuraAppliedEvent);
    }

    private double GetSkillMultiplier(SimulationState state)
    {
        var skillPoints = state?.Config.Skills[Skill.LungingStrike];
        skillPoints += state?.Config.Gear.AllGear.Sum(g => g.LungingStrike);

        return skillPoints switch
        {
            1 => 0.33,
            2 => 0.36,
            3 => 0.39,
            4 => 0.42,
            >= 5 => 0.45,
            _ => 0.0,
        };
    }
}
