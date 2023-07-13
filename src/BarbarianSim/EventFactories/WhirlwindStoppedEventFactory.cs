using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class WhirlwindStoppedEventFactory
{
    public WhirlwindStoppedEvent Create(double timestamp) => new(timestamp);
}
