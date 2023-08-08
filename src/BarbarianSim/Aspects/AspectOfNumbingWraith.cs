using BarbarianSim.Config;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Aspects;

public class AspectOfNumbingWraith : Aspect, IHandlesEvent<FuryGeneratedEvent>
{
    // Each point of Fury generated while at Maximum Fury grants 0-54 Fortify
    public int Fortify { get; set; }

    public AspectOfNumbingWraith(MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public void ProcessEvent(FuryGeneratedEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            var originalFortify = state.Player.Fortify;
            state.Player.Fortify += e.OverflowFury * Fortify;
            state.Player.Fortify = Math.Min(_maxLifeCalculator.Calculate(state), state.Player.Fortify);

            if (state.Player.Fortify != originalFortify)
            {
                _log.Verbose($"Aspect of Numbing Wraith increased Fortify by {state.Player.Fortify - originalFortify:F2}%");
            }
        }
    }
}
