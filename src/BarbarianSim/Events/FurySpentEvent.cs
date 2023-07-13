using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class FurySpentEvent : EventInfo
{
    public FurySpentEvent(FuryCostReductionCalculator furyCostReductionCalculator, double timestamp, double furySpent, SkillType skillType) : base(timestamp)
    {
        _furyCostReductionCalculator = furyCostReductionCalculator;
        BaseFurySpent = furySpent;
        SkillType = skillType;
    }

    private readonly FuryCostReductionCalculator _furyCostReductionCalculator;

    public double BaseFurySpent { get; init; }
    public double FurySpent { get; set; }
    public SkillType SkillType { get; init; }

    public override void ProcessEvent(SimulationState state)
    {
        FurySpent = BaseFurySpent * _furyCostReductionCalculator.Calculate(state, SkillType);

        if (FurySpent > state.Player.Fury)
        {
            throw new Exception("Not enough fury to spend");
        }

        state.Player.Fury -= FurySpent;
    }

    public override string ToString() => $"{base.ToString()} - {FurySpent} fury spent";
}
