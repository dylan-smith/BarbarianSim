namespace BarbarianSim.StatCalculators;

public class DamageToInjuredCalculator
{
    public virtual double Calculate(SimulationState state, EnemyState enemy) => enemy.IsInjured() ? state.Config.Gear.GetStatTotal(g => g.DamageToInjured) : 0.0;
}
