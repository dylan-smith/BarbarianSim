namespace BarbarianSim.StatCalculators;

public class DamageToInjuredCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, EnemyState enemy) => Calculate<DamageToInjuredCalculator>(state, enemy);

    protected override double InstanceCalculate(SimulationState state, EnemyState enemy) => enemy.IsInjured() ? state.Config.Gear.GetStatTotal(g => g.DamageToInjured) : 0.0;
}
