using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Abilities;

public class Whirlwind
{
    public const double FURY_COST = 25.0;
    public const double LUCKY_HIT_CHANCE = 0.2;

    public Whirlwind(FuryCostReductionCalculator furyCostReductionCalculator,
                     WhirlwindRefreshEventFactory whirlwindRefreshEventFactory,
                     AuraAppliedEventFactory auraAppliedEventFactory)
    {
        _furyCostReductionCalculator = furyCostReductionCalculator;
        _whirlwindRefreshEventFactory = whirlwindRefreshEventFactory;
        _auraAppliedEventFactory = auraAppliedEventFactory;
    }

    private readonly FuryCostReductionCalculator _furyCostReductionCalculator;
    private readonly WhirlwindRefreshEventFactory _whirlwindRefreshEventFactory;
    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    public bool CanUse(SimulationState state)
    {
        return !state.Player.Auras.Contains(Aura.WeaponCooldown) &&
               state.Player.Fury >= (FURY_COST * _furyCostReductionCalculator.Calculate(state, SkillType.Core));
    }

    public void Use(SimulationState state) => state.Events.Add(_whirlwindRefreshEventFactory.Create(state.CurrentTime));

    public void StopSpinning(SimulationState state)
    {
        state.Events.Add(_auraAppliedEventFactory.Create(state.CurrentTime, 0, Aura.StoppingWhirlwind));
    }
}
