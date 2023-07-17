namespace BarbarianSim.StatCalculators;

public class DamageToSlowedCalculator
{
    public virtual double Calculate(SimulationState state, EnemyState enemy) => enemy.IsSlowed() ? state.Config.Gear.GetStatTotal(g => g.DamageToSlowed) : 0.0;
}
