using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class LuckyHitEvent : EventInfo
{
    public LuckyHitEvent(double timestamp, SkillType skillType, EnemyState target) : base(timestamp)
    {
        SkillType = skillType;
        Target = target;
    }

    public SkillType SkillType { get; init; }
    public EnemyState Target { get; init; }

    public override void ProcessEvent(SimulationState state)
    {
        // Do nothing, other things will subscribe to this event
    }
}
