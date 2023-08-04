using BarbarianSim.Aspects;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class RamaladnisMagnumOpusEventHandler : EventHandler<RamaladnisMagnumOpusEvent>
{
    public override void ProcessEvent(RamaladnisMagnumOpusEvent e, SimulationState state)
    {
        e.FurySpentEvent = new FurySpentEvent(e.Timestamp, "Ramaladni's Magnum Opus", RamaladnisMagnumOpus.FURY_PER_SECOND_LOST, SkillType.None);
        state.Events.Add(e.FurySpentEvent);

        e.NextRamaladnisMagnumOpusEvent = new RamaladnisMagnumOpusEvent(e.Timestamp + 1);
        state.Events.Add(e.NextRamaladnisMagnumOpusEvent);
    }
}
