﻿using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class FuryGeneratedEventHandler : EventHandler<FuryGeneratedEvent>
{
    public FuryGeneratedEventHandler(ResourceGenerationCalculator resourceGenerationCalculator, MaxFuryCalculator maxFuryCalculator, SimLogger log)
    {
        _resourceGenerationCalculator = resourceGenerationCalculator;
        _maxFuryCalculator = maxFuryCalculator;
        _log = log;
    }

    private readonly ResourceGenerationCalculator _resourceGenerationCalculator;
    private readonly MaxFuryCalculator _maxFuryCalculator;
    private readonly SimLogger _log;

    public override void ProcessEvent(FuryGeneratedEvent e, SimulationState state)
    {
        _log.Verbose($"Base Fury = {e.BaseFury}");
        var multiplier = _resourceGenerationCalculator.Calculate(state);
        e.FuryGenerated += e.BaseFury * multiplier;
        state.Player.Fury += e.FuryGenerated;

        var maxFury = _maxFuryCalculator.Calculate(state);
        if (state.Player.Fury > maxFury)
        {
            e.OverflowFury = state.Player.Fury - maxFury;
            state.Player.Fury = maxFury;
            _log.Verbose($"Overflow Fury = {e.OverflowFury:F2}");
        }
    }
}
