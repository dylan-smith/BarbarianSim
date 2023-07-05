using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class GohrsDevastatingGrips : Aspect
{
    // Whirlwind explodes after it ends, dealing X% of the total base damage dealt to surrounding enemies as Fire damage
    // Explosion damage is only increased by the first 100 hits of whirlwind (https://us.forums.blizzard.com/en/d4/t/unique-power-on-gohrs-devastating-grips-disabled/27618/2)
    public double DamagePercent { get; init; }
    public int HitCount { get; set; }
    public double TotalBaseDamage { get; set; }
    public const int MAX_HIT_COUNT = 100;

    public GohrsDevastatingGrips(double damagePercent) => DamagePercent = damagePercent;

    public void ProcessEvent(DamageEvent damageEvent, SimulationState _)
    {
        if (damageEvent.DamageSource != DamageSource.Whirlwind)
        {
            return;
        }

        if (HitCount < MAX_HIT_COUNT)
        {
            TotalBaseDamage += damageEvent.Damage;
            HitCount++;
        }
    }

    public void ProcessEvent(WhirlwindStoppedEvent whirlwindStoppedEvent, SimulationState state)
    {
        var damage = TotalBaseDamage * DamagePercent / 100.0;
        state.Events.Add(new GohrsDevastatingGripsProcEvent(whirlwindStoppedEvent.Timestamp, damage));

        HitCount = 0;
        TotalBaseDamage = 0;
    }
}
