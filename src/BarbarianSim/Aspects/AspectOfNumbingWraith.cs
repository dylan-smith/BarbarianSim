using BarbarianSim.Config;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Aspects;

public class AspectOfNumbingWraith : Aspect, IHandlesEvent<FuryGeneratedEvent>
{
    // Each point of Fury generated while at Maximum Fury grants 0-54 Fortify
    public int Fortify { get; set; }

    public AspectOfNumbingWraith(MaxLifeCalculator maxLifeCalculator) => _maxLifeCalculator = maxLifeCalculator;

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public void ProcessEvent(FuryGeneratedEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            state.Player.Fortify += e.OverflowFury * Fortify;
            state.Player.Fortify = Math.Min(_maxLifeCalculator.Calculate(state), state.Player.Fortify);
        }
    }
}
