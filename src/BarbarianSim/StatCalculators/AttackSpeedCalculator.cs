namespace BarbarianSim.StatCalculators;

public class AttackSpeedCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<AttackSpeedCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var attackSpeed = state.Config.Gear.AllGear.Sum(g => g.AttackSpeed);

        return 1.0 / (1.0 + (attackSpeed / 100.0));
    }
}
