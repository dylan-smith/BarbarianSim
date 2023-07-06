using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfTheProtector : Aspect
{
    // Damaging an Elite enemy grants you a Barrier absorbing up to [X] damage for 10 seconds. This effect can only happen once every 30 seconds.
    public int BarrierAmount { get; init; }

    public AspectOfTheProtector(int barrierAmount) => BarrierAmount = barrierAmount;

    public void ProcessEvent(DamageEvent _, SimulationState state)
    {
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
