namespace BarbarianSim.StatCalculators;

public class HealingReceivedCalculator
{
    public HealingReceivedCalculator(WillpowerCalculator willpowerCalculator) => _willpowerCalculator = willpowerCalculator;

    private readonly WillpowerCalculator _willpowerCalculator;

    public virtual double Calculate(SimulationState state)
    {
        var healingReceived = state.Config.Gear.GetStatTotal(g => g.HealingReceived);
        healingReceived += _willpowerCalculator.Calculate(state) * 0.1;

        return 1.0 + (healingReceived / 100.0);
    }
}
