namespace BarbarianSim.StatCalculators;

public class WillpowerCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<WillpowerCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var dexterity = state.Config.Gear.GetStatTotal(g => g.Willpower);
        dexterity += state.Config.Gear.GetStatTotal(g => g.AllStats);
        dexterity += state.Config.PlayerSettings.Willpower;
        dexterity += state.Config.PlayerSettings.Level - 1;

        return dexterity;
    }
}
