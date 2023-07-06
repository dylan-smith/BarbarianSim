namespace BarbarianSim.StatCalculators;

public class VulnerableDamageBonusCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, EnemyState target) => Calculate<VulnerableDamageBonusCalculator>(state, target);

    protected override double InstanceCalculate(SimulationState state, EnemyState target)
    {
        var vulnerableDamage = state.Config.Gear.AllGear.Sum(g => g.VulnerableDamage);

        return target.IsVulnerable() ? 1.2 + (vulnerableDamage / 100.0) : 1.0;
    }
}
