using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class ViolentWhirlwind
{
    // After using Whirlwind for 2 seconds, Whirlwind deals 30%[x] increased damage until cancelled
    public const double DELAY = 2.0;
    public const double DAMAGE_MULTIPLIER = 1.3;

    public ViolentWhirlwind(AuraAppliedEventFactory auraAppliedEventFactory) => _auraAppliedEventFactory = auraAppliedEventFactory;

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    public void ProcessEvent(WhirlwindSpinEvent whirlwindSpinEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.ViolentWhirlwind))
        {
            state.Events.Add(_auraAppliedEventFactory.Create(whirlwindSpinEvent.Timestamp + DELAY, 0, Aura.ViolentWhirlwind));
        }
    }
}
