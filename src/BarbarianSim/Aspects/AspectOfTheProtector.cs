using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfTheProtector : Aspect
{
    // Damaging an Elite enemy grants you a Barrier absorbing up to [X] damage for 10 seconds. This effect can only happen once every 30 seconds.
    public int BarrierAmount { get; init; }

    public AspectOfTheProtector(AspectOfTheProtectorProcEventFactory aspectOfTheProtectorProcEventFactory, int barrierAmount)
    {
        _aspectOfTheProtectorProcEventFactory = aspectOfTheProtectorProcEventFactory;
        BarrierAmount = barrierAmount;
    }

    private readonly AspectOfTheProtectorProcEventFactory _aspectOfTheProtectorProcEventFactory;
    
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

        state.Events.Add(_aspectOfTheProtectorProcEventFactory.Create(state.CurrentTime, BarrierAmount));
    }
}
