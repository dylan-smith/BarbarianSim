namespace BarbarianSim.StatCalculators;

public class DamageToInjuredCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<DamageToInjuredCalculator>(state);

    protected override double InstanceCalculate(SimulationState state) => state.Enemy.IsInjured() ? state.Config.Gear.GetStatTotal(g => g.DamageToInjured) : 0.0;
}
