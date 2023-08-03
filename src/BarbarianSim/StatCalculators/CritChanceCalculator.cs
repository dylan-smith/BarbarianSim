using BarbarianSim.Aspects;
using BarbarianSim.Config;
using BarbarianSim.Enums;

namespace BarbarianSim.StatCalculators;

public class CritChanceCalculator
{
    public CritChanceCalculator(CritChancePhysicalAgainstElitesCalculator critChancePhysicalAgainstElitesCalculator,
                                CritChanceVulnerableCalculator critChanceVulnerableCalculator,
                                DexterityCalculator dexterityCalculator,
                                AspectOfTheDireWhirlwind aspectOfTheDireWhirlwind,
                                SmitingAspect smitingAspect)
    {
        _critChancePhysicalAgainstElitesCalculator = critChancePhysicalAgainstElitesCalculator;
        _critChanceVulnerableCalculator = critChanceVulnerableCalculator;
        _dexterityCalculator = dexterityCalculator;
        _aspectOfTheDireWhirlwind = aspectOfTheDireWhirlwind;
        _smitingAspect = smitingAspect;
    }

    private readonly CritChancePhysicalAgainstElitesCalculator _critChancePhysicalAgainstElitesCalculator;
    private readonly CritChanceVulnerableCalculator _critChanceVulnerableCalculator;
    private readonly DexterityCalculator _dexterityCalculator;
    private readonly AspectOfTheDireWhirlwind _aspectOfTheDireWhirlwind;
    private readonly SmitingAspect _smitingAspect;

    public virtual double Calculate(SimulationState state, DamageType damageType, EnemyState enemy, GearItem weapon)
    {
        var critChance = 5.0; // base chance to crit
        critChance += state.Config.GetStatTotal(g => g.CritChance);
        critChance += _critChancePhysicalAgainstElitesCalculator.Calculate(state, damageType);
        critChance += _critChanceVulnerableCalculator.Calculate(state, enemy, weapon);
        critChance += _dexterityCalculator.Calculate(state) * 0.02;
        critChance += _aspectOfTheDireWhirlwind.GetCritChanceBonus(state);

        critChance *= _smitingAspect.GetCriticalStrikeChanceBonus(state, enemy);

        return critChance / 100.0;
    }
}
