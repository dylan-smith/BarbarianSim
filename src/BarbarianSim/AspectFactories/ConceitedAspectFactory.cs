using BarbarianSim.Aspects;

namespace BarbarianSim.EventFactories;

public class ConceitedAspectFactory
{
    public ConceitedAspect Create(int damage) => new(damage);
}
