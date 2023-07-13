using BarbarianSim.Aspects;

namespace BarbarianSim.EventFactories;

public class BoldChieftainsAspectFactory
{
    public BoldChieftainsAspect Create(double cooldown) => new(cooldown);
}
