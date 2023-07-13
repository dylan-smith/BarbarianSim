using BarbarianSim.Aspects;

namespace BarbarianSim.EventFactories;

public class RageOfHarrogathFactory
{
    public RageOfHarrogath Create(int chance) => new(chance);
}
