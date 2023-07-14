namespace BarbarianSim.StatCalculators;

public class VulnerableDamageBonusCalculator
{
    public double Calculate(SimulationState state, EnemyState enemy)
    {
        var vulnerableDamage = state.Config.Gear.AllGear.Sum(g => g.VulnerableDamage);

        return enemy.IsVulnerable() ? 1.2 + (vulnerableDamage / 100.0) : 1.0;
    }
}
