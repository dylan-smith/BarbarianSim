namespace BarbarianSim.StatCalculators;

public class LuckyHitChanceCalculator
{
    public double Calculate(SimulationState state) => state.Config.Gear.AllGear.Sum(g => g.LuckyHitChance) / 100.0;
}
