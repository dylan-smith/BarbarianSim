using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class GohrsDevastatingGripsProcEventHandler : EventHandler<GohrsDevastatingGripsProcEvent>
{
    public GohrsDevastatingGripsProcEventHandler(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public override void ProcessEvent(GohrsDevastatingGripsProcEvent e, SimulationState state)
    {
        foreach (var enemy in state.Enemies)
        {
            var damageEvent = new DamageEvent(e.Timestamp, "Gohr's Devastatic Grips", e.Damage, DamageType.Fire, DamageSource.GohrsDevastatingGrips, SkillType.None, enemy);
            e.DamageEvents.Add(damageEvent);
            state.Events.Add(damageEvent);
            _log.Verbose($"Created DamageEvent for {e.Damage:F2} Fire damage on Enemy #{enemy.Id}");
        }
    }
}
