using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class CritDamageCalculator
{
    public CritDamageCalculator(HeavyHanded heavyHanded) => _heavyHanded = heavyHanded;

    private readonly HeavyHanded _heavyHanded;

    public virtual double Calculate(SimulationState state, Expertise expertise)
    {
        var critDamage = state.Config.Gear.GetStatTotal(g => g.CritDamage);
        critDamage += _heavyHanded.GetCriticalStrikeDamage(state, expertise);

        return 1.5 + (critDamage / 100.0);
    }
}
