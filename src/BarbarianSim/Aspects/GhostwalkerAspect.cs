using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class GhostwalkerAspect : Aspect, IHandlesEvent<AuraAppliedEvent>
{
    // While Unstoppable and for 4 seconds after, you gain 10-25%[+] increased Movement Speed and can move freely through enemies
    public int Speed { get; set; }

    public GhostwalkerAspect(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(AuraAppliedEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state) && e.Aura == Aura.Unstoppable)
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, "Ghostwalker Aspect", e.Duration + 4.0, Aura.Ghostwalker));
            _log.Verbose($"Ghostwalker Aspect created AuraAppliedEvent for Ghostwalker for {e.Duration + 4.0:F2} seconds");
        }
    }

    public virtual double GetMovementSpeedIncrease(SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            if (state.Player.Auras.Contains(Aura.Ghostwalker))
            {
                return Speed;
            }
        }

        return 0;
    }
}
