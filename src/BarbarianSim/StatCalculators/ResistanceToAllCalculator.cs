namespace BarbarianSim.StatCalculators;

public class ResistanceToAllCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state) => Calculate<ResistanceToAllCalculator>(state);

    protected override double InstanceCalculate(SimulationState state)
    {
        var resistance = state.Config.Gear.GetStatTotal(g => g.ResistanceToAll);
        resistance += IntelligenceCalculator.Calculate(state) * 0.05;

        return resistance / 100.0;
    }
}
