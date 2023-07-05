using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class FurySpentEvent : EventInfo
{
    public FurySpentEvent(double timestamp, double furySpent) : base(timestamp) => BaseFurySpent = furySpent;

    public double BaseFurySpent { get; init; }
    public double FurySpent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        FurySpent = BaseFurySpent * FuryCostReductionCalculator.Calculate(state);

        if (FurySpent > state.Player.Fury)
        {
            throw new Exception("Not enough fury to spend");
        }

        state.Player.Fury -= FurySpent;
    }

    public override string ToString() => $"{base.ToString()} - {FurySpent} fury spent";
}
