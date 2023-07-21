using BarbarianSim.Aspects;
using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class CritChanceCalculator
{
    public CritChanceCalculator(CritChancePhysicalAgainstElitesCalculator critChancePhysicalAgainstElitesCalculator,
                                DexterityCalculator dexterityCalculator,
                                AspectOfTheDireWhirlwind aspectOfTheDireWhirlwind)
    {
        _critChancePhysicalAgainstElitesCalculator = critChancePhysicalAgainstElitesCalculator;
        _dexterityCalculator = dexterityCalculator;
        _aspectOfTheDireWhirlwind = aspectOfTheDireWhirlwind;
    }

    private readonly CritChancePhysicalAgainstElitesCalculator _critChancePhysicalAgainstElitesCalculator;
    private readonly DexterityCalculator _dexterityCalculator;
    private readonly AspectOfTheDireWhirlwind _aspectOfTheDireWhirlwind;

    public virtual double Calculate(SimulationState state, DamageType damageType)
    {
        var critChance = 5.0; // base chance to crit
        critChance += state.Config.Gear.AllGear.Sum(g => g.CritChance);
        critChance += _critChancePhysicalAgainstElitesCalculator.Calculate(state, damageType);
        critChance += _dexterityCalculator.Calculate(state) * 0.02;
        critChance += _aspectOfTheDireWhirlwind.GetCritChanceBonus(state);

        return critChance / 100.0;
    }
}
