using BarbarianSim.Abilities;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventFactories;

public class IronSkinEventFactory
{
    public IronSkinEventFactory(AuraAppliedEventFactory auraAppliedEventFactory,
                                MaxLifeCalculator maxLifeCalculator,
                                IronSkin ironSkin,
                                BarrierAppliedEventFactory barrierAppliedEventFactory,
                                HealingEventFactory healingEventFactory,
                                FortifyGeneratedEventFactory fortifyGeneratedEventFactory)
    {
        _auraAppliedEventFactory = auraAppliedEventFactory;
        _maxLifeCalculator = maxLifeCalculator;
        _ironSkin = ironSkin;
        _barrierAppliedEventFactory = barrierAppliedEventFactory;
        _healingEventFactory = healingEventFactory;
        _fortifyGeneratedEventFactory = fortifyGeneratedEventFactory;
    }

    private readonly AuraAppliedEventFactory _auraAppliedEventFactory;
    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly IronSkin _ironSkin;
    private readonly BarrierAppliedEventFactory _barrierAppliedEventFactory;
    private readonly HealingEventFactory _healingEventFactory;
    private readonly FortifyGeneratedEventFactory _fortifyGeneratedEventFactory;

    public IronSkinEvent Create(double timestamp) =>
        new(_auraAppliedEventFactory, _maxLifeCalculator, _ironSkin, _barrierAppliedEventFactory, _healingEventFactory, _fortifyGeneratedEventFactory, timestamp);
}
