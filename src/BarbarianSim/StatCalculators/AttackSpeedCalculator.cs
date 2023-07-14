namespace BarbarianSim.StatCalculators;

public class AttackSpeedCalculator
{
    public double Calculate(SimulationState state)
    {
        var attackSpeed = state.Config.Gear.AllGear.Sum(g => g.AttackSpeed);

        return 1.0 / (1.0 + (attackSpeed / 100.0));
    }
}
