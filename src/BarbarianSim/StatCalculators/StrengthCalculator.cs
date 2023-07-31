namespace BarbarianSim.StatCalculators;

public class StrengthCalculator
{
    public virtual double Calculate(SimulationState state)
    {
        var strength = state.Config.GetStatTotal(g => g.Strength);
        strength += state.Config.GetStatTotal(g => g.AllStats);
        strength += state.Config.PlayerSettings.Strength;
        strength += state.Config.PlayerSettings.Level - 1;

        return strength;
    }
}
