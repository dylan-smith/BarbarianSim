using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class CritChanceCalculator
{
    public CritChanceCalculator(CritChancePhysicalAgainstElitesCalculator critChancePhysicalAgainstElitesCalculator,
                                DexterityCalculator dexterityCalculator)
    {
        _critChancePhysicalAgainstElitesCalculator = critChancePhysicalAgainstElitesCalculator;
        _dexterityCalculator = dexterityCalculator;
    }

    private readonly CritChancePhysicalAgainstElitesCalculator _critChancePhysicalAgainstElitesCalculator;
    private readonly DexterityCalculator _dexterityCalculator;

    public double Calculate(SimulationState state, DamageType damageType)
    {
        var critChance = 5.0; // base chance to crit
        critChance += state.Config.Gear.AllGear.Sum(g => g.CritChance);
        critChance += _critChancePhysicalAgainstElitesCalculator.Calculate(state, damageType);
        critChance += _dexterityCalculator.Calculate(state) * 0.02;

        return critChance / 100.0;
    }
}
