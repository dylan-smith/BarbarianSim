namespace BarbarianSim.StatCalculators;

public class ResistanceToAllCalculator
{
    public ResistanceToAllCalculator(IntelligenceCalculator intelligenceCalculator, SimLogger log)
    {
        _intelligenceCalculator = intelligenceCalculator;
        _log = log;
    }

    private readonly IntelligenceCalculator _intelligenceCalculator;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var resistance = state.Config.GetStatTotal(g => g.ResistanceToAll);
        if (resistance > 0)
        {
            _log.Verbose($"Resistance to All from Config = {resistance:F2}%");
        }

        resistance += _intelligenceCalculator.Calculate(state) * 0.05;

        var result = resistance / 100.0;
        if (result > 0)
        {
            _log.Verbose($"Total Resistance to All = {result:P2}");
        }

        return result;
    }
}
