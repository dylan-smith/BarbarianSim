using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class GohrsDevastatingGripsProcEventFactory
{
    public GohrsDevastatingGripsProcEvent Create(double timestamp, double damage) => new(timestamp, damage);
}
