namespace BarbarianSim.StatCalculators;

public class DamageReductionFromCloseCalculator
{
    public virtual double Calculate(SimulationState state) => state.Config.Gear.GetStatTotalMultiplied(g => 1 - (g.DamageReductionFromClose / 100));
}
