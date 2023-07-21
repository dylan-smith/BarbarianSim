using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class FuryGeneratedEventHandler : EventHandler<FuryGeneratedEvent>
{
    public FuryGeneratedEventHandler(ResourceGenerationCalculator resourceGenerationCalculator, MaxFuryCalculator maxFuryCalculator)
    {
        _resourceGenerationCalculator = resourceGenerationCalculator;
        _maxFuryCalculator = maxFuryCalculator;
    }

    private readonly ResourceGenerationCalculator _resourceGenerationCalculator;
    private readonly MaxFuryCalculator _maxFuryCalculator;

    public override void ProcessEvent(FuryGeneratedEvent e, SimulationState state)
    {
        var multiplier = _resourceGenerationCalculator.Calculate(state);
        e.FuryGenerated += e.BaseFury * multiplier;
        state.Player.Fury += e.FuryGenerated;

        if (state.Player.Fury > _maxFuryCalculator.Calculate(state))
        {
            e.OverflowFury = state.Player.Fury - _maxFuryCalculator.Calculate(state);
            state.Player.Fury = _maxFuryCalculator.Calculate(state);
        }
    }
}
