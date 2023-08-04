using BarbarianSim.Arsenal;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class TwoHandedMaceExpertiseProcEventHandler : EventHandler<TwoHandedMaceExpertiseProcEvent>
{
    public override void ProcessEvent(TwoHandedMaceExpertiseProcEvent e, SimulationState state)
    {
        var fury = state.Player.Auras.Contains(Aura.Berserking) ? TwoHandedMaceExpertise.FURY_GENERATED * 2 : TwoHandedMaceExpertise.FURY_GENERATED;
        e.FuryGeneratedEvent = new FuryGeneratedEvent(e.Timestamp, "Two-Handed Mace Expertise", fury);
        state.Events.Add(e.FuryGeneratedEvent);
    }
}
