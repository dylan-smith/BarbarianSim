namespace BarbarianSim.StatCalculators;

public class AttackSpeedCalculator
{
    public virtual double Calculate(SimulationState state)
    {
        var attackSpeed = state.Config.GetStatTotal(g => g.AttackSpeed);

        return 1.0 / (1.0 + (attackSpeed / 100.0));
    }
}
