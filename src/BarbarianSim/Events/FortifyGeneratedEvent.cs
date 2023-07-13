using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class FortifyGeneratedEvent : EventInfo
{
    public double Amount { get; init; }

    public FortifyGeneratedEvent(MaxLifeCalculator maxLifeCalculator, double timestamp, double amount) : base(timestamp)
    {
        _maxLifeCalculator = maxLifeCalculator;
        Amount = amount;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    
    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Fortify += Amount;
        state.Player.Fortify = Math.Min(_maxLifeCalculator.Calculate(state), state.Player.Fortify);
    }
}
