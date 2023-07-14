using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class CritDamageCalculator
{
    public double Calculate(SimulationState state, Expertise expertise)
    {
        var critDamage = state.Config.Gear.AllGear.Sum(g => g.CritDamage);
        critDamage += HeavyHanded.GetCriticalStrikeDamage(state, expertise);

        return 1.5 + (critDamage / 100.0);
    }
}
