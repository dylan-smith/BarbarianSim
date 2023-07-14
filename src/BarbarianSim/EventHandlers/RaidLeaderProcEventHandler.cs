using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class RaidLeaderProcEventHandler : EventHandler<RaidLeaderProcEvent>
{
    public RaidLeaderProcEventHandler(MaxLifeCalculator maxLifeCalculator, RaidLeader raidLeader)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _raidLeader = raidLeader;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly RaidLeader _raidLeader;

    public override void ProcessEvent(RaidLeaderProcEvent e, SimulationState state)
    {
        for (var i = 0; i < Math.Floor(e.Duration); i++)
        {
            var healEvent = new HealingEvent(e.Timestamp + i + 1, _maxLifeCalculator.Calculate(state) * _raidLeader.GetHealPercentage(state));
            e.HealingEvents.Add(healEvent);
            state.Events.Add(healEvent);
        }
    }
}
