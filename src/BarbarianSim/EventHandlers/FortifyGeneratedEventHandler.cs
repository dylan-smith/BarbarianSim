using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class FortifyGeneratedEventHandler : EventHandler<FortifyGeneratedEvent>
{
    public FortifyGeneratedEventHandler(MaxLifeCalculator maxLifeCalculator) => _maxLifeCalculator = maxLifeCalculator;

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public override void ProcessEvent(FortifyGeneratedEvent e, SimulationState state)
    {
        state.Player.Fortify += e.Amount;
        state.Player.Fortify = Math.Min(_maxLifeCalculator.Calculate(state), state.Player.Fortify);
    }
}
