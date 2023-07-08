using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class BerserkingAppliedEvent : EventInfo
{
    public BerserkingAppliedEvent(double timestamp, double duration) : base(timestamp) => Duration = duration;

    public double Duration { get; set; }

    public AuraExpiredEvent BerserkingExpiredEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.Berserking);

        BerserkingExpiredEvent = new AuraExpiredEvent(Timestamp + Duration, Aura.Berserking);
        state.Events.Add(BerserkingExpiredEvent);
    }
}
