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
                                SmitingAspect smitingAspect,
                                SimLogger log)
    {
        _critChancePhysicalAgainstElitesCalculator = critChancePhysicalAgainstElitesCalculator;
        _critChanceVulnerableCalculator = critChanceVulnerableCalculator;
        _dexterityCalculator = dexterityCalculator;
        _aspectOfTheDireWhirlwind = aspectOfTheDireWhirlwind;
        _smitingAspect = smitingAspect;
        _log = log;
    }

    private readonly CritChancePhysicalAgainstElitesCalculator _critChancePhysicalAgainstElitesCalculator;
    private readonly CritChanceVulnerableCalculator _critChanceVulnerableCalculator;
    private readonly DexterityCalculator _dexterityCalculator;
    private readonly AspectOfTheDireWhirlwind _aspectOfTheDireWhirlwind;
    private readonly SmitingAspect _smitingAspect;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, DamageType damageType, EnemyState enemy, GearItem weapon)
    {
        var baseCritChance = 5.0; // base chance to crit
        _log.Verbose($"Base Crit Chance = {baseCritChance:F2}%");

        var critFromConfig = state.Config.GetStatTotal(g => g.CritChance);
        if (critFromConfig > 0)
        {
            _log.Verbose($"Crit Chance from Config = {critFromConfig:F2}%");
        }

        var critPhysicalAgainstElites = _critChancePhysicalAgainstElitesCalculator.Calculate(state, damageType);
        var critVulnerable = _critChanceVulnerableCalculator.Calculate(state, enemy, weapon);
        var critDexterity = _dexterityCalculator.Calculate(state) * 0.02;
        if (critDexterity > 0)
        {
            _log.Verbose($"Crit Chance from Dexterity = {critDexterity:F2}%");
        }

        var critDireWhirlwind = _aspectOfTheDireWhirlwind.GetCritChanceBonus(state);

        var smitingMultiplier = _smitingAspect.GetCriticalStrikeChanceBonus(state, enemy);

        var result = (baseCritChance + critFromConfig + critPhysicalAgainstElites + critVulnerable + critDexterity + critDireWhirlwind) * smitingMultiplier;
        result /= 100.0;

        _log.Verbose($"Total Crit Chance = {result:P2}");

        return result;
    }
}
