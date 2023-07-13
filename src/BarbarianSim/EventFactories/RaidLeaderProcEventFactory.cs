using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventFactories;

public class RaidLeaderProcEventFactory
{
    public RaidLeaderProcEventFactory(MaxLifeCalculator maxLifeCalculator, RaidLeader raidLeader, HealingEventFactory healingEventFactory)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _raidLeader = raidLeader;
        _healingEventFactory = healingEventFactory;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly RaidLeader _raidLeader;
    private readonly HealingEventFactory _healingEventFactory;

    public RaidLeaderProcEvent Create(double timestamp, double duration) => new(_maxLifeCalculator, _raidLeader, _healingEventFactory, timestamp, duration);
}
