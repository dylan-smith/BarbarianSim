using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class StrategicRallyingCry : IHandlesEvent<RallyingCryEvent>, IHandlesEvent<DirectDamageEvent>
{
    // Rallying Cry grants you 10% Base Life (10%[x] HP) as Fortify. While Rallying Cry is active, you gain an additional 2% Base Life (2%[x] HP) as Fortify each time you take or deal Direct Damage
    public const double FORTIFY = 0.1;
    public const double DIRECT_DAMAGE_FORTIFY = 0.02;

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.StrategicRallyingCry) && state.Config.Skills[Skill.StrategicRallyingCry] > 0)
        {
            state.Events.Add(new FortifyGeneratedEvent(e.Timestamp, FORTIFY * state.Player.BaseLife));
        }
    }

    public void ProcessEvent(DirectDamageEvent e, SimulationState state)
    {
        if (state.Config.Skills.TryGetValue(Skill.StrategicRallyingCry, out var skillPoints) &&
            skillPoints > 0 &&
            state.Player.Auras.Contains(Aura.RallyingCry))
        {
            state.Events.Add(new FortifyGeneratedEvent(e.Timestamp, DIRECT_DAMAGE_FORTIFY * state.Player.BaseLife));
        }
    }
}
