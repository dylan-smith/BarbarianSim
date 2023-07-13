using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class FuryGeneratedEvent : EventInfo
{
    public double BaseFury { get; init; }
    public double FuryGenerated { get; set; }

    public FuryGeneratedEvent(ResourceGenerationCalculator resourceGenerationCalculator,
                              MaxFuryCalculator maxFuryCalculator,
                              double timestamp, double fury) : base(timestamp)
    {
        _resourceGenerationCalculator = resourceGenerationCalculator;
        _maxFuryCalculator = maxFuryCalculator;
        BaseFury = fury;
    }

    private readonly ResourceGenerationCalculator _resourceGenerationCalculator;
    private readonly MaxFuryCalculator _maxFuryCalculator;

    public override void ProcessEvent(SimulationState state)
    {
        var multiplier = _resourceGenerationCalculator.Calculate(state);
        FuryGenerated += BaseFury * multiplier;
        state.Player.Fury += FuryGenerated;

        state.Player.Fury = Math.Min(_maxFuryCalculator.Calculate(state), state.Player.Fury);
    }

    public override string ToString() => $"{base.ToString()} - {FuryGenerated} fury generated";
}
