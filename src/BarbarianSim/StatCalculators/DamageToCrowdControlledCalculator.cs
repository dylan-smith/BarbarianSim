namespace BarbarianSim.StatCalculators;

public class DamageToCrowdControlledCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<DamageToCrowdControlledCalculator>(state);

    protected override double InstanceCalculate(SimulationState state) => state.Enemy.IsCrowdControlled() ? state.Config.Gear.GetStatTotal(g => g.DamageToCrowdControlled) : 0.0;
}
