namespace BarbarianSim.StatCalculators;

public class StrengthCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<StrengthCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var strength = state.Config.Gear.GetStatTotal(g => g.Strength);
        strength += state.Config.Gear.GetStatTotal(g => g.AllStats);
        strength += state.Config.PlayerSettings.Strength;
        strength += state.Config.PlayerSettings.Level - 1;

        return strength;
    }
}
