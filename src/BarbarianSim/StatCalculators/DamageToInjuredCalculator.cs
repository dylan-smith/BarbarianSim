namespace BarbarianSim.StatCalculators;

public class DamageToInjuredCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, EnemyState target) => Calculate<DamageToInjuredCalculator>(state, target);

    protected override double InstanceCalculate(SimulationState state, EnemyState target) => target.IsInjured() ? state.Config.Gear.GetStatTotal(g => g.DamageToInjured) : 0.0;
}
