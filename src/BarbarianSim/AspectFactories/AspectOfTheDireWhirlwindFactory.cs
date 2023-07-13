using BarbarianSim.Aspects;

namespace BarbarianSim.EventFactories;

public class AspectOfTheDireWhirlwindFactory
{
    public AspectOfTheDireWhirlwind Create(int critChance, int maxCritChance) => new(critChance, maxCritChance);
}
