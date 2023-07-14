using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class FurySpentEventHandler : EventHandler<FurySpentEvent>
{
    public override void ProcessEvent(FurySpentEvent e, SimulationState state)
    {
        e.FurySpent = e.BaseFurySpent * FuryCostReductionCalculator.Calculate(state, e.SkillType);

        if (e.FurySpent > state.Player.Fury)
        {
            throw new Exception("Not enough fury to spend");
        }

        state.Player.Fury -= e.FurySpent;
    }
}
