using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class GhostwalkerAspect : Aspect, IHandlesEvent<AuraAppliedEvent>
{
    // While Unstoppable and for 4 seconds after, you gain 10-25%[+] increased Movement Speed and can move freely through enemies
    public int Speed { get; set; }

    public void ProcessEvent(AuraAppliedEvent e, SimulationState state)
    {
        if (e.Aura == Aura.Unstoppable)
        {
            state.Events.Add(new AuraAppliedEvent(e.Timestamp, e.Duration + 4.0, Aura.Ghostwalker));
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
