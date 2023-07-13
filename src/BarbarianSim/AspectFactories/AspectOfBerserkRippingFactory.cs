using BarbarianSim.Aspects;

namespace BarbarianSim.EventFactories;

public class AspectOfBerserkRippingFactory
{
    public AspectOfBerserkRipping Create(int damage) => new(damage);
}
