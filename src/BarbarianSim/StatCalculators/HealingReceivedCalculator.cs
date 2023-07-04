namespace BarbarianSim.StatCalculators;

public class HealingReceivedCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<HealingReceivedCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var healingReceived = state.Config.Gear.GetStatTotal(g => g.HealingReceived);
        healingReceived += WillpowerCalculator.Calculate(state) * 0.1;

        return 1.0 + (healingReceived / 100.0);
    }
}
