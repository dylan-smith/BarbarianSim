namespace BarbarianSim.StatCalculators;

public class LuckyHitChanceCalculator
{
    public virtual double Calculate(SimulationState state) => state.Config.GetStatTotal(g => g.LuckyHitChance) / 100.0;
}
