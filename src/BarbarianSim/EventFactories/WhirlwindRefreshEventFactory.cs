using BarbarianSim.Abilities;
using BarbarianSim.Events;

namespace BarbarianSim.EventFactories;

public class WhirlwindRefreshEventFactory
{
    public WhirlwindRefreshEventFactory(Whirlwind whirlwind,
                                        WhirlwindSpinEventFactory whirlwindSpinEventFactory,
                                        WhirlwindStoppedEventFactory whirlwindStoppedEventFactory)
    {
        _whirlwind = whirlwind;
        _whirlwindSpinEventFactory = whirlwindSpinEventFactory;
        _whirlwindStoppedEventFactory = whirlwindStoppedEventFactory;
    }

    private readonly Whirlwind _whirlwind;
    private readonly WhirlwindSpinEventFactory _whirlwindSpinEventFactory;
    private readonly WhirlwindStoppedEventFactory _whirlwindStoppedEventFactory;

    public WhirlwindRefreshEvent Create(double timestamp) => 
        new(_whirlwind, _whirlwindSpinEventFactory, _whirlwindStoppedEventFactory, timestamp);
}
