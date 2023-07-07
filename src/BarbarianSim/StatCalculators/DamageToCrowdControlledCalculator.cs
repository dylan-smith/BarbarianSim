namespace BarbarianSim.StatCalculators;

public class DamageToCrowdControlledCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, EnemyState enemy) => Calculate<DamageToCrowdControlledCalculator>(state, enemy);

    protected override double InstanceCalculate(SimulationState state, EnemyState enemy) => enemy.IsCrowdControlled() ? state.Config.Gear.GetStatTotal(g => g.DamageToCrowdControlled) : 0.0;
}
