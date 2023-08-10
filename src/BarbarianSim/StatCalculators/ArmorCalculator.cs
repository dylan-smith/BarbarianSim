using BarbarianSim.Aspects;

namespace BarbarianSim.StatCalculators;

public class ArmorCalculator
{
    public ArmorCalculator(StrengthCalculator strengthCalculator, AspectOfDisobedience aspectOfDisobedience, SimLogger log)
    {
        _strengthCalculator = strengthCalculator;
        _aspectOfDisobedience = aspectOfDisobedience;
        _log = log;
    }

    private readonly StrengthCalculator _strengthCalculator;
    private readonly AspectOfDisobedience _aspectOfDisobedience;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state)
    {
        var armor = state.Config.GetStatTotal(g => g.Armor);
        _log.Verbose($"Armor from config = {armor:F2}");

        var strength = _strengthCalculator.Calculate(state);
        armor += strength;
        _log.Verbose($"Armor from Strength = {strength}");

        var totalArmorMultiplier = 1 + (state.Config.GetStatTotal(g => g.TotalArmor) / 100.0);
        armor *= totalArmorMultiplier;

        if (totalArmorMultiplier > 1)
        {
            _log.Verbose($"Total Armor Multiplier = {totalArmorMultiplier:F2}x");
        }

        var aspectOfDisobedienceMultiplier = _aspectOfDisobedience.GetArmorBonus(state);
        armor *= aspectOfDisobedienceMultiplier;

        if (aspectOfDisobedienceMultiplier > 1)
        {
            _log.Verbose($"Aspect of Disobedience Armor Multiplier = {aspectOfDisobedienceMultiplier:F2}x");
        }

        return armor;
    }
}
