namespace BarbarianSim.StatCalculators;

public class IntelligenceCalculator
{
    public virtual double Calculate(SimulationState state)
    {
        var intelligence = state.Config.GetStatTotal(g => g.Intelligence);
        intelligence += state.Config.GetStatTotal(g => g.AllStats);
        intelligence += state.Config.PlayerSettings.Intelligence;
        intelligence += state.Config.PlayerSettings.Level - 1;

        return intelligence;
    }
}
