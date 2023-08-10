using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class EnhancedRallyingCry : IHandlesEvent<RallyingCryEvent>
{
    // Rallying Cry grants you Unstoppable while active
    public EnhancedRallyingCry(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(RallyingCryEvent e, SimulationState state)
    {
        if (state.Config.GetSkillPoints(Skill.EnhancedRallyingCry) > 0)
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, "Enhanced Rallying Cry", e.Duration, Aura.Unstoppable));
            _log.Verbose($"Enhanced Rallying Cry created AuraAppliedEvent for Unstoppable for {e.Duration:F2} seconds");
        }
    }
}
