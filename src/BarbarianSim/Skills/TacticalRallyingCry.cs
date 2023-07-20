using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class TacticalRallyingCry : IHandlesEvent<RallyingCryEvent>
{
    // Rallying Cry generates 20 fury, and grants you an additional 20%[x] Resource Generation
    public const double RESOURCE_GENERATION = 1.20;
    public const double FURY_GENERATED = 20.0;

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (state.Config.Skills.TryGetValue(Skill.TacticalRallyingCry, out var skillPoints) && skillPoints > 0)
        {
            state.Events.Add(new FuryGeneratedEvent(e.Timestamp, FURY_GENERATED));
        }
    }

    public virtual double GetResourceGeneration(SimulationState state)
    {
        return state.Config.Skills.TryGetValue(Skill.TacticalRallyingCry, out var skillPoints) &&
               skillPoints > 0 &&
               state.Player.Auras.Contains(Aura.RallyingCry)
            ? RESOURCE_GENERATION
            : 1.0;
    }
}
