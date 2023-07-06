namespace BarbarianSim.StatCalculators;

public class DamageToSlowedCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, EnemyState target) => Calculate<DamageToSlowedCalculator>(state, target);

    protected override double InstanceCalculate(SimulationState state, EnemyState target) => target.IsSlowed() ? state.Config.Gear.GetStatTotal(g => g.DamageToSlowed) : 0.0;
}
