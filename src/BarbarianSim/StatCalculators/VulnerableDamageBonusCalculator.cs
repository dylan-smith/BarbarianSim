namespace BarbarianSim.StatCalculators;

public class VulnerableDamageBonusCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<VulnerableDamageBonusCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var vulnerableDamage = state.Config.Gear.AllGear.Sum(g => g.VulnerableDamage);

        return state.Enemy.IsVulnerable() ? 1.2 + (vulnerableDamage / 100.0) : 1.0;
    }
}
