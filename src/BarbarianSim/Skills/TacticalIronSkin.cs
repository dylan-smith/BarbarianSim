using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class TacticalIronSkin : IHandlesEvent<Events.IronSkinEvent>
{
    // Tactical: While Ironskin is active Heal for 10% of the Barrier's original amount as Life per second
    public const double HEAL_PERCENT = 0.1;
    public TacticalIronSkin(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(IronSkinEvent e, SimulationState state)
    {
        if (state.Config.HasSkill(Skill.TacticalIronSkin))
        {
            for (var i = 0; i < IronSkin.DURATION; i++)
            {
                var healAmount = e.BarrierAppliedEvent.BarrierAmount * HEAL_PERCENT;
                var healEvent = new HealingEvent(e.Timestamp + i + 1, "Tactical Iron Skin", healAmount);
                state.Events.Add(healEvent);
                _log.Verbose($"Tactical Iron Skin created HealingEvent for {healAmount:F2} at Timestamp {e.Timestamp + i + 1:F2}");
            }
        }
    }
}
