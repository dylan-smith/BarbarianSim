using BarbarianSim.Config;

namespace BarbarianSim.Aspects;

public class AspectOfLimitlessRage : Aspect
{
    public AspectOfLimitlessRage(int damage, int maxDamage)
    {
        Damage = damage;
        MaxDamage = maxDamage;
    }

    public int Damage { get; init; }
    public int MaxDamage { get; init; }
}
