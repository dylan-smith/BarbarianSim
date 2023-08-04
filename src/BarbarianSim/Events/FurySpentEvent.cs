using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class FurySpentEvent : EventInfo
{
    public FurySpentEvent(double timestamp, string source, double furySpent, SkillType skillType) : base(timestamp, source)
    {
        BaseFurySpent = furySpent;
        SkillType = skillType;
    }

    public double BaseFurySpent { get; init; }
    public double FurySpent { get; set; }
    public SkillType SkillType { get; init; }

    public override string ToString() => $"{base.ToString()} - {FurySpent:F2} fury spent (Source: {Source})";
}
