using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class FuryGeneratedEvent : EventInfo
{
    public double BaseFury { get; init; }
    public double FuryGenerated { get; set; }

    public FuryGeneratedEvent(double timestamp, double fury) : base(timestamp) => BaseFury = fury;

    public override void ProcessEvent(SimulationState state)
    {
        var multiplier = ResourceGenerationCalculator.Calculate(state);
        FuryGenerated += BaseFury * multiplier;
        state.Player.Fury += FuryGenerated;

        state.Player.Fury = Math.Min(state.Player.MaxFury, state.Player.Fury);
    }

    public override string ToString() => $"{base.ToString()} - {FuryGenerated} fury generated";
}
