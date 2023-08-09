using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class FurySpentEventHandler : EventHandler<FurySpentEvent>
{
    public FurySpentEventHandler(FuryCostReductionCalculator furyCostReductionCalculator, SimLogger log)
    {
        _furyCostReductionCalculator = furyCostReductionCalculator;
        _log = log;
    }

    private readonly FuryCostReductionCalculator _furyCostReductionCalculator;
    private readonly SimLogger _log;

    public override void ProcessEvent(FurySpentEvent e, SimulationState state)
    {
        _log.Verbose($"Base Fury Spent = {e.BaseFurySpent:F2}");
        e.FurySpent = e.BaseFurySpent * _furyCostReductionCalculator.Calculate(state, e.SkillType);

        state.Player.Fury -= e.FurySpent;
        state.Player.Fury = Math.Max(state.Player.Fury, 0);
        _log.Verbose($"Fury Remaining = {state.Player.Fury}");
    }
}
