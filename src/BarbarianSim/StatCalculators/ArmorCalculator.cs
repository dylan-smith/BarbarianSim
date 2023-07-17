namespace BarbarianSim.StatCalculators;

public class ArmorCalculator
{
    public ArmorCalculator(StrengthCalculator strengthCalculator) => _strengthCalculator = strengthCalculator;

    private readonly StrengthCalculator _strengthCalculator;

    public virtual double Calculate(SimulationState state)
    {
        var armor = state.Config.Gear.GetStatTotal(g => g.Armor);
        armor += _strengthCalculator.Calculate(state);

        return armor;
    }
}
