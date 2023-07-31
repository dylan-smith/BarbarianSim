namespace BarbarianSim.StatCalculators;

public class ResistanceToAllCalculator
{
    public ResistanceToAllCalculator(IntelligenceCalculator intelligenceCalculator) => _intelligenceCalculator = intelligenceCalculator;

    private readonly IntelligenceCalculator _intelligenceCalculator;

    public virtual double Calculate(SimulationState state)
    {
        var resistance = state.Config.GetStatTotal(g => g.ResistanceToAll);
        resistance += _intelligenceCalculator.Calculate(state) * 0.05;

        return resistance / 100.0;
    }
}
