using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class Hamstring
{
    public Hamstring(AuraAppliedEventFactory auraAppliedEventFactory) => _auraAppliedEventFactory = auraAppliedEventFactory;

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;

    public void ProcessEvent(BleedAppliedEvent bleedAppliedEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.Hamstring))
        {
            state.Events.Add(_auraAppliedEventFactory.Create(bleedAppliedEvent.Timestamp, bleedAppliedEvent.Duration, Aura.Slow, bleedAppliedEvent.Target));
        }
    }
}
