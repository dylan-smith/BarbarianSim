namespace BarbarianSim.StatCalculators;

public class WillpowerCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<WillpowerCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var willpower = state.Config.Gear.GetStatTotal(g => g.Willpower);
        willpower += state.Config.Gear.GetStatTotal(g => g.AllStats);
        willpower += state.Config.PlayerSettings.Willpower;
        willpower += state.Config.PlayerSettings.Level - 1;

        return willpower;
    }
}
