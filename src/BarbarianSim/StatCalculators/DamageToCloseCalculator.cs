namespace BarbarianSim.StatCalculators;

public class DamageToCloseCalculator
{
    public virtual double Calculate(SimulationState state) => state.Config.GetStatTotal(g => g.DamageToClose);
}
