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

    public void ProcessEvent(DirectDamageEvent directDamageEvent, SimulationState _)
    {
        if (directDamageEvent.DamageSource != DamageSource.Whirlwind)
        {
            return;
        }

        if (HitCount < MAX_HIT_COUNT)
        {
            TotalBaseDamage += directDamageEvent.BaseDamage;
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
