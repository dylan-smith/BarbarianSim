using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class WhirlwindSpinEventHandler : EventHandler<WhirlwindSpinEvent>
{
    // Rapidly attack surrounding enemies for 17%[x] damage (Fury Cost: 11, Lucky Hit: 20%)
    public override void ProcessEvent(WhirlwindSpinEvent e, SimulationState state)
    {
        e.WhirlwindingAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, 0, Aura.Whirlwinding);
        state.Events.Add(e.WhirlwindingAuraAppliedEvent);

        e.FurySpentEvent = new FurySpentEvent(e.Timestamp, Whirlwind.FURY_COST, SkillType.Core);
        state.Events.Add(e.FurySpentEvent);

        var weaponDamage = (Whirlwind.Weapon.MinDamage + Whirlwind.Weapon.MaxDamage) / 2.0;
        var skillMultiplier = Whirlwind.GetSkillMultiplier(state);
        e.BaseDamage = weaponDamage * skillMultiplier;

        foreach (var enemy in state.Enemies)
        {
            var directDamageEvent = new DirectDamageEvent(e.Timestamp, e.BaseDamage, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, Whirlwind.LUCKY_HIT_CHANCE, Whirlwind.Weapon.Expertise, enemy);
            e.DirectDamageEvents.Add(directDamageEvent);
            state.Events.Add(directDamageEvent);
        }

        var weaponSpeed = 1 / Whirlwind.Weapon.AttacksPerSecond;
        weaponSpeed *= AttackSpeedCalculator.Calculate(state);
        e.WeaponCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(e.WeaponCooldownAuraAppliedEvent);

        e.WhirlwindRefreshEvent = new WhirlwindRefreshEvent(e.Timestamp + weaponSpeed);
        state.Events.Add(e.WhirlwindRefreshEvent);
    }
}
