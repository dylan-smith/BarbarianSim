using BarbarianSim.Arsenal;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class CritDamageCalculator
{
    public CritDamageCalculator(HeavyHanded heavyHanded, TwoHandedMaceExpertise twoHandedMaceExpertise, SimLogger log)
    {
        _heavyHanded = heavyHanded;
        _twoHandedMaceExpertise = twoHandedMaceExpertise;
        _log = log;
    }

    private readonly HeavyHanded _heavyHanded;
    private readonly TwoHandedMaceExpertise _twoHandedMaceExpertise;
    private readonly SimLogger _log;

    public virtual double Calculate(SimulationState state, Expertise expertise, GearItem weapon, EnemyState enemy)
    {
        var baseCritDamage = 50.0;
        _log.Verbose($"Base Critical Strike Damage = {baseCritDamage:F2}%");

        var critFromConfig = state.Config.GetStatTotal(g => g.CritDamage);
        _log.Verbose($"Critical Strike Damage from Config = {critFromConfig:F2}%");

        var critFromHeavyHanded = _heavyHanded.GetCriticalStrikeDamage(state, expertise);

        var critFromTwoHandedMaceExpertise = _twoHandedMaceExpertise.GetCriticalStrikeDamageMultiplier(state, weapon, enemy);

        var critDamage = 1 + ((baseCritDamage + critFromConfig + critFromHeavyHanded) / 100.0);
        critDamage *= critFromTwoHandedMaceExpertise;

        _log.Verbose($"Total Critical Strike Damage = {critDamage:F2}x");

        return critDamage;
    }
}
