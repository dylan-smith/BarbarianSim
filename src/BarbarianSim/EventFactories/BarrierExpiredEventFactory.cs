using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class BarrierExpiredEventFactory
{
    public BarrierExpiredEvent Create(double timestamp, Barrier barrier) => new(timestamp, barrier);
}
