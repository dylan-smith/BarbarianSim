namespace BarbarianSim.StatCalculators;

public class DamageToCloseCalculator
{
    public virtual double Calculate(SimulationState state) => state.Config.Gear.GetStatTotal(g => g.DamageToClose);
}
