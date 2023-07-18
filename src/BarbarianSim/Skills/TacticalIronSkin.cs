using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class TacticalIronSkin : IHandlesEvent<Events.IronSkinEvent>
{
    // Tactical: While Ironskin is active Heal for 10% of the Barrier's original amount as Life per second
    public void ProcessEvent(IronSkinEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.TacticalIronSkin) && state.Config.Skills[Skill.TacticalIronSkin] > 0)
        {
            for (var i = 0; i < IronSkin.DURATION; i++)
            {
                var healEvent = new HealingEvent(e.Timestamp + i + 1, e.BarrierAppliedEvent.BarrierAmount * IronSkin.HEAL_FROM_TACTICAL);
                state.Events.Add(healEvent);
            }
        }
    }
}
