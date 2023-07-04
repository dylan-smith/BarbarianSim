namespace BarbarianSim.StatCalculators;

public class OverpowerDamageCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<OverpowerDamageCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var overpowerDamage = state.Config.Gear.GetStatTotal(g => g.OverpowerDamage);
        overpowerDamage += WillpowerCalculator.Calculate(state) * 0.25;

        return 1.0 + (overpowerDamage / 100.0);
    }
}
