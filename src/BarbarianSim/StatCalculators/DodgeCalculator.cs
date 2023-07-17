namespace BarbarianSim.StatCalculators;

public class DodgeCalculator
{
    public DodgeCalculator(DexterityCalculator dexterityCalculator)
    {
        _dexterityCalculator = dexterityCalculator;
    }

    private readonly DexterityCalculator _dexterityCalculator;

    public virtual double Calculate(SimulationState state)
    {
        var dodge = state.Config.Gear.GetStatTotal(g => g.Dodge);
        dodge += _dexterityCalculator.Calculate(state) * 0.01;

        return dodge / 100.0;
    }
}
