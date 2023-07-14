using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class RaidLeaderProcEvent : EventInfo
{
    public RaidLeaderProcEvent(double timestamp, double duration) : base(timestamp) => Duration = duration;

    public double Duration { get; set; }
    public IList<HealingEvent> HealingEvents { get; init; } = new List<HealingEvent>();
}
