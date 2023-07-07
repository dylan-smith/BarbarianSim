namespace BarbarianSim.StatCalculators;

public class DamageToSlowedCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, EnemyState enemy) => Calculate<DamageToSlowedCalculator>(state, enemy);

    protected override double InstanceCalculate(SimulationState state, EnemyState enemy) => enemy.IsSlowed() ? state.Config.Gear.GetStatTotal(g => g.DamageToSlowed) : 0.0;
}
