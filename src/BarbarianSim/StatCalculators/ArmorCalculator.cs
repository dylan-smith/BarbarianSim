using BarbarianSim.Aspects;

namespace BarbarianSim.StatCalculators;

public class ArmorCalculator
{
    public ArmorCalculator(StrengthCalculator strengthCalculator, AspectOfDisobedience aspectOfDisobedience)
    {
        _strengthCalculator = strengthCalculator;
        _aspectOfDisobedience = aspectOfDisobedience;
    }

    private readonly StrengthCalculator _strengthCalculator;
    private readonly AspectOfDisobedience _aspectOfDisobedience;

    public virtual double Calculate(SimulationState state)
    {
        var armor = state.Config.GetStatTotal(g => g.Armor);
        armor += _strengthCalculator.Calculate(state);

        armor *= 1 + (state.Config.GetStatTotal(g => g.TotalArmor) / 100.0);
        armor *= _aspectOfDisobedience.GetArmorBonus(state);

        return armor;
    }
}
