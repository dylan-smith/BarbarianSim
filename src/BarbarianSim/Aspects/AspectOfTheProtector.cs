using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfTheProtector : Aspect, IHandlesEvent<DamageEvent>
{
    // Damaging an Elite enemy grants you a Barrier absorbing up to [X] damage for 10 seconds. This effect can only happen once every 30 seconds.
    public int BarrierAmount { get; set; }

    public const double BARRIER_EXPIRY = 10.0;
    public const double COOLDOWN = 30;

    public void ProcessEvent(DamageEvent e, SimulationState state)
    {
        if (!IsAspectEquipped(state))
        {
            return;
        }

        if (!state.Config.EnemySettings.IsElite)
        {
            return;
        }

        if (state.Player.Auras.Contains(Aura.AspectOfTheProtectorCooldown))
        {
            return;
        }

        if (state.Events.Any(e => e is AspectOfTheProtectorProcEvent))
        {
            return;
        }

        state.Events.Add(new AspectOfTheProtectorProcEvent(state.CurrentTime, BarrierAmount));
    }
}
