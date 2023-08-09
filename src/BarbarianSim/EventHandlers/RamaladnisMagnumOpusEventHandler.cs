using BarbarianSim.Aspects;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class RamaladnisMagnumOpusEventHandler : EventHandler<RamaladnisMagnumOpusEvent>
{
    public RamaladnisMagnumOpusEventHandler(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public override void ProcessEvent(RamaladnisMagnumOpusEvent e, SimulationState state)
    {
        e.FurySpentEvent = new FurySpentEvent(e.Timestamp, "Ramaladni's Magnum Opus", RamaladnisMagnumOpus.FURY_PER_SECOND_LOST, SkillType.None);
        state.Events.Add(e.FurySpentEvent);
        _log.Verbose($"Created FurySpentEvent for {RamaladnisMagnumOpus.FURY_PER_SECOND_LOST:F2} Fury");

        e.NextRamaladnisMagnumOpusEvent = new RamaladnisMagnumOpusEvent(e.Timestamp + 1);
        state.Events.Add(e.NextRamaladnisMagnumOpusEvent);
        _log.Verbose($"Created RamaladnisMagnumOpusEvent for timestamp {e.NextRamaladnisMagnumOpusEvent.Timestamp:F2}");
    }
}
