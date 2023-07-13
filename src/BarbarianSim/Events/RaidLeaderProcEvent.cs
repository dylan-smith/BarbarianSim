using BarbarianSim.EventFactories;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class RaidLeaderProcEvent : EventInfo
{
    public RaidLeaderProcEvent(MaxLifeCalculator maxLifeCalculator,
                               RaidLeader raidLeader,
                               HealingEventFactory healingEventFactory,
                               double timestamp,
                               double duration) : base(timestamp)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _raidLeader = raidLeader;
        _healingEventFactory = healingEventFactory;
        Duration = duration;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly RaidLeader _raidLeader;
    private readonly HealingEventFactory _healingEventFactory;

    public double Duration { get; set; }
    public IList<HealingEvent> HealingEvents { get; init; } = new List<HealingEvent>();

    public override void ProcessEvent(SimulationState state)
    {
        for (var i = 0; i < Math.Floor(Duration); i++)
        {
            var healEvent = _healingEventFactory.Create(Timestamp + i + 1, _maxLifeCalculator.Calculate(state) * _raidLeader.GetHealPercentage(state));
            HealingEvents.Add(healEvent);
            state.Events.Add(healEvent);
        }
    }
}
