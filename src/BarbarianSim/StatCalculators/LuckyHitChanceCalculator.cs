namespace BarbarianSim.StatCalculators;

public class LuckyHitChanceCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<LuckyHitChanceCalculator>(state);

    protected override double InstanceCalculate(SimulationState state) => state.Config.Gear.AllGear.Sum(g => g.LuckyHitChance) / 100.0;
}
