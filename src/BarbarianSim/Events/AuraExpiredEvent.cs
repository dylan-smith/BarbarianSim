using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class AuraExpiredEvent : EventInfo
{
    public AuraExpiredEvent(double timestamp, Aura aura) : base(timestamp) => Aura = aura;

    public AuraExpiredEvent(double timestamp, EnemyState enemy, Aura aura) : base(timestamp)
    {
        Aura = aura;
        Enemy = enemy;
    }

    public Aura Aura { get; set; }
    public EnemyState Enemy { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        if (Enemy == null)
        {
            // if there are other events it means there's an Aura been applied with a later expiration time
            if (!state.Events.Any(e => e is AuraExpiredEvent expiredEvent && expiredEvent.Aura == Aura))
            {
                state.Player.Auras.Remove(Aura);
            }
        }
        else
        {
            if (!state.Events.Any(e => e is AuraExpiredEvent expiredEvent && expiredEvent.Enemy == Enemy && expiredEvent.Aura == Aura))
            {
                Enemy.Auras.Remove(Aura);
            }
        }
    }
}
