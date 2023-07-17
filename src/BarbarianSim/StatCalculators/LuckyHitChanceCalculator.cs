namespace BarbarianSim.StatCalculators;

public class LuckyHitChanceCalculator
{
    public virtual double Calculate(SimulationState state) => state.Config.Gear.GetStatTotal(g => g.LuckyHitChance) / 100.0;
}
