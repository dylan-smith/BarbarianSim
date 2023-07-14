using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class ChallengingShoutEvent : EventInfo
{
    public ChallengingShoutEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraAppliedEvent ChallengingShoutCooldownAuraAppliedEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }
    public double Duration { get; set; }
    public AuraAppliedEvent ChallengingShoutAuraAppliedEvent { get; set; }
    public IList<AuraAppliedEvent> TauntAuraAppliedEvent { get; init; } = new List<AuraAppliedEvent>();
}
