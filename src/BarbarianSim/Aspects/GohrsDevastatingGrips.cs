using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class GohrsDevastatingGrips : Aspect, IHandlesEvent<DirectDamageEvent>, IHandlesEvent<WhirlwindStoppedEvent>
{
    // Whirlwind explodes after it ends, dealing X% of the total base damage dealt to surrounding enemies as Fire damage
    // Explosion damage is only increased by the first 100 hits of whirlwind (https://us.forums.blizzard.com/en/d4/t/unique-power-on-gohrs-devastating-grips-disabled/27618/2)
    public double DamagePercent { get; set; }
    public int HitCount { get; set; }
    public double TotalBaseDamage { get; set; }
    public const int MAX_HIT_COUNT = 100;

    public GohrsDevastatingGrips(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(DirectDamageEvent e, SimulationState state)
    {
        if (e.DamageSource != DamageSource.Whirlwind)
        {
            return;
        }

        if (HitCount < MAX_HIT_COUNT)
        {
            TotalBaseDamage += e.BaseDamage;
            HitCount++;
        }
    }

    public void ProcessEvent(WhirlwindStoppedEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            var damage = TotalBaseDamage * DamagePercent / 100.0;
            state.Events.Add(new GohrsDevastatingGripsProcEvent(e.Timestamp, damage));
            _log.Verbose($"Gohr's Devastating Grips created GohrsDevastatingGripsProcEvent for {damage:F2} Fire damage");

            HitCount = 0;
            TotalBaseDamage = 0;
        }
    }
}
