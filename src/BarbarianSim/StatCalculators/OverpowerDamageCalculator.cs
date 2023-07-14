namespace BarbarianSim.StatCalculators;

public class OverpowerDamageCalculator
{
    public OverpowerDamageCalculator(WillpowerCalculator willpowerCalculator) => _willpowerCalculator = willpowerCalculator;

    private readonly WillpowerCalculator _willpowerCalculator;

    public double Calculate(SimulationState state)
    {
        var overpowerDamage = state.Config.Gear.GetStatTotal(g => g.OverpowerDamage);
        overpowerDamage += _willpowerCalculator.Calculate(state) * 0.25;

        return 1.0 + (overpowerDamage / 100.0);
    }
}
