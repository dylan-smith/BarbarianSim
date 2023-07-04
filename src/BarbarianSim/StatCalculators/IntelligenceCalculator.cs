namespace BarbarianSim.StatCalculators;

public class IntelligenceCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<IntelligenceCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var intelligence = state.Config.Gear.GetStatTotal(g => g.Intelligence);
        intelligence += state.Config.Gear.GetStatTotal(g => g.AllStats);
        intelligence += state.Config.PlayerSettings.Intelligence;
        intelligence += state.Config.PlayerSettings.Level - 1;

        return intelligence;
    }
}
