using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.Events;

public class WarCryEvent : EventInfo
{
    public WarCryEvent(double timestamp) : base(timestamp)
    {
    }

    public AuraAppliedEvent WarCryAuraAppliedEvent { get; set; }
    public AuraAppliedEvent WarCryCooldownAuraAppliedEvent { get; set; }
    public AuraAppliedEvent BerserkingAuraAppliedEvent { get; set; }
    public FortifyGeneratedEvent FortifyGeneratedEvent { get; set; }
    public RaidLeaderProcEvent RaidLeaderProcEvent { get; set; }
    public double Duration { get; set; }
}
