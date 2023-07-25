using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class AuraAppliedEventHandler : EventHandler<AuraAppliedEvent>
{
    public AuraAppliedEventHandler(CrowdControlDurationCalculator crowdControlDurationCalculator) => _crowdControlDurationCalculator = crowdControlDurationCalculator;

    private readonly CrowdControlDurationCalculator _crowdControlDurationCalculator;

    public override void ProcessEvent(AuraAppliedEvent e, SimulationState state)
    {
        if (e.Target == null)
        {
            state.Player.Auras.Add(e.Aura);
        }
        else
        {
            e.Target.Auras.Add(e.Aura);
        }

        if (e.Duration > 0)
        {
            var duration = e.Duration;

            if (e.Aura.IsCrowdControl())
            {
                duration = _crowdControlDurationCalculator.Calculate(state, duration);
            }

            e.AuraExpiredEvent = new AuraExpiredEvent(e.Timestamp + duration, e.Target, e.Aura);
            state.Events.Add(e.AuraExpiredEvent);
        }
    }
}
