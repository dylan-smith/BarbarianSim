using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Paragon;

public class Warbringer : IHandlesEvent<FurySpentEvent>
{
    // For every 75 Fury you spend, gain 12% of your Maximum Life (12% x [HP]) as Fortify.
    public const double FORTIFY_BONUS = 0.12;
    public const double FURY_SPENT_FOR_PROC = 75;

    public Warbringer(MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public void ProcessEvent(FurySpentEvent e, SimulationState state)
    {
        if (!state.Config.HasParagonNode(ParagonNode.Warbringer))
        {
            return;
        }

        var startEvent = state.ProcessedEvents.OrderBy(e => e.Timestamp).LastOrDefault(e => e is WarbringerProcEvent);
        var startTime = startEvent?.Timestamp ?? -1;
        _log.Verbose($"Last Warbringer Proc was Timestamp {startTime:F2}");

        var totalFurySpent = state.ProcessedEvents.OfType<FurySpentEvent>().Where(e => e.Timestamp > startTime).Sum(e => e.FurySpent);
        _log.Verbose($"Total Fury Spent since last Warbringer Proc = {totalFurySpent:F2}");

        if (totalFurySpent >= FURY_SPENT_FOR_PROC)
        {
            state.Events.Add(new WarbringerProcEvent(e.Timestamp));
            _log.Verbose($"Warbringer created WarbringerProcEvent");
        }
    }

    public virtual double GetFortifyGenerated(SimulationState state)
    {
        var maxLife = _maxLifeCalculator.Calculate(state);
        var result = FORTIFY_BONUS * maxLife;

        _log.Verbose($"Warbringer Fortify Bonus = {result:F2}");
        return result;
    }
}
