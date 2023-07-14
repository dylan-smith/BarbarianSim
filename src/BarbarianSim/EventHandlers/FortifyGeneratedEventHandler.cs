using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class FortifyGeneratedEventHandler : EventHandler<FortifyGeneratedEvent>
{
    public override void ProcessEvent(FortifyGeneratedEvent e, SimulationState state)
    {
        state.Player.Fortify += e.Amount;
        state.Player.Fortify = Math.Min(MaxLifeCalculator.Calculate(state), state.Player.Fortify);
    }
}
