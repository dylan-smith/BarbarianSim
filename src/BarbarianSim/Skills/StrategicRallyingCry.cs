using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class StrategicRallyingCry : IHandlesEvent<RallyingCryEvent>
{
    // Rallying Cry grants you 10% Base Life (10%[x] HP) as Fortify. While Rallying Cry is active, you gain an additional 2% Base Life (2%[x] HP) as Fortify each time you take or deal Direct Damage
    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.StrategicRallyingCry) && state.Config.Skills[Skill.StrategicRallyingCry] > 0)
        {
            state.Events.Add(new FortifyGeneratedEvent(e.Timestamp, RallyingCry.FORTIFY_FROM_STRATEGIC_RALLYING_CRY * state.Player.BaseLife));
        }
    }
}
