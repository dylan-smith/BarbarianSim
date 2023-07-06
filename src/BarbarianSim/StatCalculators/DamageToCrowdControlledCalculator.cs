namespace BarbarianSim.StatCalculators;

public class DamageToCrowdControlledCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, EnemyState target) => Calculate<DamageToCrowdControlledCalculator>(state, target);

    protected override double InstanceCalculate(SimulationState state, EnemyState target) => target.IsCrowdControlled() ? state.Config.Gear.GetStatTotal(g => g.DamageToCrowdControlled) : 0.0;
}
