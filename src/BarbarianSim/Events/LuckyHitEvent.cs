using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class LuckyHitEvent : EventInfo
{
    public LuckyHitEvent(double timestamp, SkillType skillType) : base(timestamp) => SkillType = skillType;

    public SkillType SkillType { get; init; }

    public override void ProcessEvent(SimulationState state)
    {
        // Do nothing, other things will subscribe to this event
    }
}
