using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class LungingStrikeEventHandler : EventHandler<LungingStrikeEvent>
{
    public override void ProcessEvent(LungingStrikeEvent e, SimulationState state)
    {
        e.FuryGeneratedEvent = new FuryGeneratedEvent(e.Timestamp, LungingStrike.FURY_GENERATED);
        state.Events.Add(e.FuryGeneratedEvent);

        var weaponDamage = (LungingStrike.Weapon.MinDamage + LungingStrike.Weapon.MaxDamage) / 2.0;
        var skillMultiplier = LungingStrike.GetSkillMultiplier(state);
        e.BaseDamage = weaponDamage * skillMultiplier;

        e.DirectDamageEvent = new DirectDamageEvent(e.Timestamp, e.BaseDamage, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, LungingStrike.LUCKY_HIT_CHANCE, LungingStrike.Weapon.Expertise, e.Target);
        state.Events.Add(e.DirectDamageEvent);

        var weaponSpeed = 1 / LungingStrike.Weapon.AttacksPerSecond;
        weaponSpeed *= AttackSpeedCalculator.Calculate(state);
        e.WeaponCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(e.WeaponCooldownAuraAppliedEvent);
    }
}
