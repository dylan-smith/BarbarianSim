using BarbarianSim.Events;
using BarbarianSim.Skills;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class RaidLeaderProcEventHandler : EventHandler<RaidLeaderProcEvent>
{
    public override void ProcessEvent(RaidLeaderProcEvent e, SimulationState state)
    {
        for (var i = 0; i < Math.Floor(e.Duration); i++)
        {
            var healEvent = new HealingEvent(e.Timestamp + i + 1, MaxLifeCalculator.Calculate(state) * RaidLeader.GetHealPercentage(state));
            e.HealingEvents.Add(healEvent);
            state.Events.Add(healEvent);
        }
    }
}
