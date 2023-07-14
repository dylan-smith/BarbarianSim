using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;

namespace BarbarianSim.Events;

public class WhirlwindSpinEvent : EventInfo
{
    // Rapidly attack surrounding enemies for 17%[x] damage (Fury Cost: 11, Lucky Hit: 20%)
    public WhirlwindSpinEvent(AuraAppliedEventFactory auraAppliedEventFactory,
                              FurySpentEventFactory furySpentEventFactory,
                              DirectDamageEventFactory directDamageEventFactory,
                              double timestamp) : base(timestamp)
    {
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _furySpentEventFactory = furySpentEventFactory;
        _directDamageEventFactory = directDamageEventFactory;
    }

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly FurySpentEventFactory _furySpentEventFactory;
    private readonly DirectDamageEventFactory _directDamageEventFactory;

    public AuraAppliedEvent WhirlwindingAuraAppliedEvent { get; set; }
    public IList<DirectDamageEvent> DirectDamageEvents { get; init; } = new List<DirectDamageEvent>();
    public IList<FuryGeneratedEvent> FuryGeneratedEvents { get; init; } = new List<FuryGeneratedEvent>();
    public FurySpentEvent FurySpentEvent { get; set; }

    public double BaseDamage { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        FurySpentEvent = _furySpentEventFactory.Create(Timestamp, Whirlwind.FURY_COST, SkillType.Core);
        state.Events.Add(FurySpentEvent);

        var weaponDamage = (state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind].MinDamage + state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind].MaxDamage) / 2.0;
        var skillMultiplier = GetSkillMultiplier(state);
        BaseDamage = weaponDamage * skillMultiplier;

        foreach (var enemy in state.Enemies)
        {
            var directDamageEvent = _directDamageEventFactory.Create(Timestamp, BaseDamage, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, Whirlwind.LUCKY_HIT_CHANCE, state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind].Expertise, enemy);
            DirectDamageEvents.Add(directDamageEvent);
            state.Events.Add(directDamageEvent);
        }
    }

    private double GetSkillMultiplier(SimulationState state)
    {
        var skillPoints = state?.Config.Skills[Skill.Whirlwind];
        skillPoints += state?.Config.Gear.AllGear.Sum(g => g.Whirlwind);

        return skillPoints switch
        {
            1 => 0.17,
            2 => 0.19,
            3 => 0.21,
            4 => 0.23,
            >= 5 => 0.24,
            _ => 0.0,
        };
    }
}
