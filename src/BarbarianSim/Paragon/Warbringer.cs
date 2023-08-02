using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Paragon;

public class Warbringer : IHandlesEvent<FurySpentEvent>
{
    // For every 75 Fury you spend, gain 12% of your Maximum Life (12% x [HP]) as Fortify.
    public const double FORTIFY_BONUS = 0.12;
    public const double FURY_SPENT_FOR_PROC = 75;

    public Warbringer(MaxLifeCalculator maxLifeCalculator)
    {
        _maxLifeCalculator = maxLifeCalculator;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public void ProcessEvent(FurySpentEvent e, SimulationState state)
    {
        if (!state.Config.HasParagonNode(ParagonNode.Warbringer))
        {
            return;
        }

        var startEvent = state.ProcessedEvents.OrderBy(e => e.Timestamp).LastOrDefault(e => e is WarbringerProcEvent);
        var startTime = startEvent?.Timestamp ?? -1;

        var totalFurySpent = state.ProcessedEvents.OfType<FurySpentEvent>().Where(e => e.Timestamp > startTime).Sum(e => e.FurySpent);

        if (totalFurySpent >= FURY_SPENT_FOR_PROC)
        {
            state.Events.Add(new WarbringerProcEvent(e.Timestamp));
        }
    }

    public virtual double GetFortifyGenerated(SimulationState state) => FORTIFY_BONUS * _maxLifeCalculator.Calculate(state);
}
