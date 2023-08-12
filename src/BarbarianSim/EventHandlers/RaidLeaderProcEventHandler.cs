using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class RaidLeaderProcEventHandler : EventHandler<RaidLeaderProcEvent>
{
    public RaidLeaderProcEventHandler(MaxLifeCalculator maxLifeCalculator, RaidLeader raidLeader, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _raidLeader = raidLeader;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly RaidLeader _raidLeader;
    private readonly SimLogger _log;

    public override void ProcessEvent(RaidLeaderProcEvent e, SimulationState state)
    {
        var maxLife = _maxLifeCalculator.Calculate(state);
        var healPercent = _raidLeader.GetHealPercentage(state);

        for (var i = 0; i < Math.Floor(e.Duration); i++)
        {
            var healEvent = new HealingEvent(e.Timestamp + i + 1, "Raid Leader", maxLife * healPercent);
            e.HealingEvents.Add(healEvent);
            state.Events.Add(healEvent);
            _log.Verbose($"Created HealingEvent for {healEvent.BaseAmountHealed:F2} at Timestamp {e.Timestamp + i + 1:F2}");
        }
    }
}
