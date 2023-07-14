using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class WhirlwindRefreshEvent : EventInfo
{
    public WhirlwindRefreshEvent(WhirlwindSpinEventFactory whirlwindSpinEventFactory,
                                 WhirlwindRefreshEventFactory whirlwindRefreshEventFactory,
                                 WhirlwindStoppedEventFactory whirlwindStoppedEventFactory,
                                 FuryCostReductionCalculator furyCostReductionCalculator,
                                 AttackSpeedCalculator attackSpeedCalculator,
                                 AuraAppliedEventFactory auraAppliedEventFactory,
                                 double timestamp) : base(timestamp)
    {
        _whirlwindSpinEventFactory = whirlwindSpinEventFactory;
        _whirlwindRefreshEventFactory = whirlwindRefreshEventFactory;
        _whirlwindStoppedEventFactory = whirlwindStoppedEventFactory;
        _furyCostReductionCalculator = furyCostReductionCalculator;
        _attackSpeedCalculator = attackSpeedCalculator;
        _auraAppliedEventFactory = auraAppliedEventFactory;
    }

    private readonly WhirlwindSpinEventFactory _whirlwindSpinEventFactory;
    private readonly WhirlwindRefreshEventFactory _whirlwindRefreshEventFactory;
    private readonly WhirlwindStoppedEventFactory _whirlwindStoppedEventFactory;
    private readonly FuryCostReductionCalculator _furyCostReductionCalculator;
    private readonly AttackSpeedCalculator _attackSpeedCalculator;
    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    public WhirlwindSpinEvent WhirlwindSpinEvent { get; set; }
    public WhirlwindRefreshEvent NextWhirlwindRefreshEvent { get; set; }
    public WhirlwindStoppedEvent WhirlwindStoppedEvent { get; set; }
    public AuraAppliedEvent WeaponCooldownAuraAppliedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        if (CanRefresh(state))
        {
            WhirlwindSpinEvent = _whirlwindSpinEventFactory.Create(Timestamp);
            state.Events.Add(WhirlwindSpinEvent);

            var weaponSpeed = 1 / state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind].AttacksPerSecond;
            weaponSpeed *= _attackSpeedCalculator.Calculate(state);
            WeaponCooldownAuraAppliedEvent = _auraAppliedEventFactory.Create(Timestamp, weaponSpeed, Aura.WeaponCooldown);
            state.Events.Add(WeaponCooldownAuraAppliedEvent);

            NextWhirlwindRefreshEvent = _whirlwindRefreshEventFactory.Create(Timestamp + weaponSpeed);
            state.Events.Add(NextWhirlwindRefreshEvent);
        }
        else
        {
            WhirlwindStoppedEvent = _whirlwindStoppedEventFactory.Create(Timestamp);
            state.Events.Add(WhirlwindStoppedEvent);
        }
    }

    private bool CanRefresh(SimulationState state) => state.Player.Fury >= (Whirlwind.FURY_COST * _furyCostReductionCalculator.Calculate(state, SkillType.Core)) && !state.Player.Auras.Contains(Aura.StoppingWhirlwind);
}
