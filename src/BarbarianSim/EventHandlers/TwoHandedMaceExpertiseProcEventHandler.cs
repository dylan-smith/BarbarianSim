using BarbarianSim.Arsenal;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class TwoHandedMaceExpertiseProcEventHandler : EventHandler<TwoHandedMaceExpertiseProcEvent>
{
    public TwoHandedMaceExpertiseProcEventHandler(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public override void ProcessEvent(TwoHandedMaceExpertiseProcEvent e, SimulationState state)
    {
        var fury = TwoHandedMaceExpertise.FURY_GENERATED;

        if (state.Player.Auras.Contains(Aura.Berserking))
        {
            fury *= 2;
            _log.Verbose($"Doubling the Fury generated while Berserking is active");
        }

        e.FuryGeneratedEvent = new FuryGeneratedEvent(e.Timestamp, "Two-Handed Mace Expertise", fury);
        state.Events.Add(e.FuryGeneratedEvent);
        _log.Verbose($"Created FuryGeneratedEvent for {fury:F2} Fury");
    }
}
