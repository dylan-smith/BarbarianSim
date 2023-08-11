using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class ThornsCalculator
{
    public ThornsCalculator(StrategicChallengingShout strategicChallengingShout, SimLogger log)
    {
        _strategicChallengingShout = strategicChallengingShout;
        _log = log;
    }

    private readonly StrategicChallengingShout _strategicChallengingShout;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var thorns = state.Config.GetStatTotal(g => g.Thorns);
        if (thorns > 0)
        {
            _log.Verbose($"Thorns from Config = {thorns:F2}%");
        }

        thorns += _strategicChallengingShout.GetThorns(state);

        if (thorns > 0)
        {
            _log.Verbose($"Total Thorns = {thorns:F2}%");
        }

        return thorns;
    }
}
