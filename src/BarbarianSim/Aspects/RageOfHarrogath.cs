using BarbarianSim.Config;

namespace BarbarianSim.Aspects;

public class RageOfHarrogath : Aspect
{
    // Lucky Hit: Up to a [20 - 40]% chance to reduce the Cooldowns of your Non-Ultimate Skills by 1.5 seconds when you inflict Bleeding on Elites.
    public int Chance { get; set; }
    public const double COOLDOWN_REDUCTION = 1.5;
}
