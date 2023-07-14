using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class FuryGeneratedEventHandler : EventHandler<FuryGeneratedEvent>
{
    public override void ProcessEvent(FuryGeneratedEvent e, SimulationState state)
    {
        var multiplier = ResourceGenerationCalculator.Calculate(state);
        e.FuryGenerated += e.BaseFury * multiplier;
        state.Player.Fury += e.FuryGenerated;

        state.Player.Fury = Math.Min(MaxFuryCalculator.Calculate(state), state.Player.Fury);
    }
}
