using BarbarianSim.Aspects;

namespace BarbarianSim.EventFactories;

public class AspectOfLimitlessRageFactory
{
    public AspectOfLimitlessRage Create(int damage, int maxDamage) => new(damage, maxDamage);
}
