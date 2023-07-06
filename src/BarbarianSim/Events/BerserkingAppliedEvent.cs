using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class BerserkingAppliedEvent : EventInfo
{
    public BerserkingAppliedEvent(double timestamp, double duration) : base(timestamp) => Duration = duration;

    public double Duration { get; set; }

    public BerserkingExpiredEvent BerserkingExpiredEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.Berserking);

        BerserkingExpiredEvent = new BerserkingExpiredEvent(Timestamp + Duration);
        state.Events.Add(BerserkingExpiredEvent);
    }
}
