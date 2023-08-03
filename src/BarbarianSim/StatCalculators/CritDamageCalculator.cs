using BarbarianSim.Arsenal;
using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class CritDamageCalculator
{
    public CritDamageCalculator(HeavyHanded heavyHanded, TwoHandedMaceExpertise twoHandedMaceExpertise)
    {
        _heavyHanded = heavyHanded;
        _twoHandedMaceExpertise = twoHandedMaceExpertise;
    }

    private readonly HeavyHanded _heavyHanded;
    private readonly TwoHandedMaceExpertise _twoHandedMaceExpertise;

    public virtual double Calculate(SimulationState state, Expertise expertise, GearItem weapon, EnemyState enemy)
    {
        var critDamage = 50.0;
        critDamage += state.Config.GetStatTotal(g => g.CritDamage);
        critDamage += _heavyHanded.GetCriticalStrikeDamage(state, expertise);

        critDamage = 1 + (critDamage / 100.0);

        critDamage *= _twoHandedMaceExpertise.GetCriticalStrikeDamageMultiplier(state, weapon, enemy);

        return critDamage;
    }
}
