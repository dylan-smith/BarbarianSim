using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class EnhancedWarCry : IHandlesEvent<WarCryEvent>
{
    // War Cry grants you Berserking for 4 seconds
    public const double BERSERKING_DURATION = 4;

    public EnhancedWarCry(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(WarCryEvent e, SimulationState state)
    {
        if (state.Config.GetSkillPoints(Skill.EnhancedWarCry) > 0)
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, "Enhanced War Cry", BERSERKING_DURATION, Aura.Berserking));
            _log.Verbose($"Enhanced War Cry created AuraAppliedEvent for Berserking for {BERSERKING_DURATION:F2} seconds");
        }
    }
}
