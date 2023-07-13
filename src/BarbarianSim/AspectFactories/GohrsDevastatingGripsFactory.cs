using BarbarianSim.Aspects;

namespace BarbarianSim.EventFactories;

public class GohrsDevastatingGripsFactory
{
    public GohrsDevastatingGrips Create(double damagePercent) => new(damagePercent);
}
