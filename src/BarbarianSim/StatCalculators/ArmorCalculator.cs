namespace BarbarianSim.StatCalculators;

public class ArmorCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<ArmorCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var armor = state.Config.Gear.GetStatTotal(g => g.Armor);
        armor += StrengthCalculator.Calculate(state);

        return armor;
    }
}
