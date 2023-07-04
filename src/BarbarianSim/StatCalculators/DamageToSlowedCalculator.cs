namespace BarbarianSim.StatCalculators;

public class DamageToSlowedCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<DamageToSlowedCalculator>(state);

    protected override double InstanceCalculate(SimulationState state) => state.Enemy.IsSlowed() ? state.Config.Gear.GetStatTotal(g => g.DamageToSlowed) : 0.0;
}
