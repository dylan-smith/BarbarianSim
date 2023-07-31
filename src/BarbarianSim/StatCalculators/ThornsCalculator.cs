using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class ThornsCalculator
{
    public ThornsCalculator(StrategicChallengingShout strategicChallengingShout) => _strategicChallengingShout = strategicChallengingShout;

    private readonly StrategicChallengingShout _strategicChallengingShout;

    public virtual double Calculate(SimulationState state)
    {
        var thorns = state.Config.GetStatTotal(g => g.Thorns);

        thorns += _strategicChallengingShout.GetThorns(state);

        return thorns;
    }
}
