using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class FurySpentEventHandler : EventHandler<FurySpentEvent>
{
    public FurySpentEventHandler(FuryCostReductionCalculator furyCostReductionCalculator) => _furyCostReductionCalculator = furyCostReductionCalculator;

    private readonly FuryCostReductionCalculator _furyCostReductionCalculator;

    public override void ProcessEvent(FurySpentEvent e, SimulationState state)
    {
        e.FurySpent = e.BaseFurySpent * _furyCostReductionCalculator.Calculate(state, e.SkillType);

        if (e.FurySpent > state.Player.Fury)
        {
            throw new Exception("Not enough fury to spend");
        }

        state.Player.Fury -= e.FurySpent;
    }
}
