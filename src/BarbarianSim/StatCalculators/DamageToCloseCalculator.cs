namespace BarbarianSim.StatCalculators;

public class DamageToCloseCalculator
{
    public double Calculate(SimulationState state) => state.Config.Gear.GetStatTotal(g => g.DamageToClose);
}
