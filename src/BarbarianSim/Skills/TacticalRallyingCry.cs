using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class TacticalRallyingCry : IHandlesEvent<RallyingCryEvent>
{
    // Rallying Cry generates 20 fury, and grants you an additional 20%[x] Resource Generation
    public const double RESOURCE_GENERATION = 1.20;
    public const double FURY_GENERATED = 20.0;

    public TacticalRallyingCry(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (state.Config.HasSkill(Skill.TacticalRallyingCry))
        {
            state.Events.Add(new FuryGeneratedEvent(e.Timestamp, "Tactical Rallying Cry", FURY_GENERATED));
            _log.Verbose($"Tactical Rallying Cry created FuryGeneratedEvent for {FURY_GENERATED:F2} Fury");
        }
    }

    public virtual double GetResourceGeneration(SimulationState state)
    {
        if (state.Config.HasSkill(Skill.TacticalRallyingCry) &&
               state.Player.Auras.Contains(Aura.RallyingCry))
        {
            _log.Verbose($"Resource Generation from Tactical Rallying Cry = {RESOURCE_GENERATION}x");
            return RESOURCE_GENERATION;
        }

        return 1.0;
    }
}
