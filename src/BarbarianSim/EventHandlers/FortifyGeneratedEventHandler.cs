using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class FortifyGeneratedEventHandler : EventHandler<FortifyGeneratedEvent>
{
    public FortifyGeneratedEventHandler(MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public override void ProcessEvent(FortifyGeneratedEvent e, SimulationState state)
    {
        var originalFortify = state.Player.Fortify;
        state.Player.Fortify += e.Amount;
        state.Player.Fortify = Math.Min(_maxLifeCalculator.Calculate(state), state.Player.Fortify);

        if (state.Player.Fortify != originalFortify)
        {
            _log.Verbose($"Player Fortify increased from {originalFortify:F2} to {state.Player.Fortify:F2}");
        }
    }
}
