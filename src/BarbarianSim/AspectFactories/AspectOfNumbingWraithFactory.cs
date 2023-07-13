using BarbarianSim.Aspects;

namespace BarbarianSim.EventFactories;

public class AspectOfNumbingWraithFactory
{
    public AspectOfNumbingWraith Create(int fortify) => new(fortify);
}
