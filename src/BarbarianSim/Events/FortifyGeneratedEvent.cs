using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class FortifyGeneratedEvent : EventInfo
{
    public double Amount { get; init; }

    public FortifyGeneratedEvent(double timestamp, double amount) : base(timestamp) => Amount = amount;

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Fortify += Amount;
        state.Player.Fortify = Math.Min(MaxLifeCalculator.Calculate(state), state.Player.Fortify);
    }
}
