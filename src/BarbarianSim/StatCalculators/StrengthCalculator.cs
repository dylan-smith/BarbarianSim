namespace BarbarianSim.StatCalculators;

public class StrengthCalculator
{
    public double Calculate(SimulationState state)
    {
        var strength = state.Config.Gear.GetStatTotal(g => g.Strength);
        strength += state.Config.Gear.GetStatTotal(g => g.AllStats);
        strength += state.Config.PlayerSettings.Strength;
        strength += state.Config.PlayerSettings.Level - 1;

        return strength;
    }
}
