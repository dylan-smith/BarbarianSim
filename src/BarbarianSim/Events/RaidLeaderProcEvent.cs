using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class RaidLeaderProcEvent : EventInfo
{
    public RaidLeaderProcEvent(double timestamp, double duration) : base(timestamp) => Duration = duration;

    public double Duration { get; set; }
    public IList<HealingEvent> HealingEvents { get; init; } = new List<HealingEvent>();

    public override void ProcessEvent(SimulationState state)
    {
        for (var i = 0; i < Math.Floor(Duration); i++)
        {
            var healEvent = new HealingEvent(Timestamp + i + 1, MaxLifeCalculator.Calculate(state) * RaidLeader.GetHealPercentage(state));
            HealingEvents.Add(healEvent);
            state.Events.Add(healEvent);
        }
    }
}
