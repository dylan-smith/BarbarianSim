using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class Hamstring : IHandlesEvent<BleedAppliedEvent>
{
    // Your Bleeding effects Slow Healthy enemies by 10%
    public Hamstring(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(BleedAppliedEvent e, SimulationState state)
    {
        if (state.Config.HasSkill(Skill.Hamstring) && e.Target.IsHealthy())
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, "Hamstring", e.Duration, Aura.Slow, e.Target));
            _log.Verbose($"Hamstring created AuraAppliedEvent for Slow for {e.Duration:F2} seconds on Enemy #{e.Target.Id}");
        }
    }
}
