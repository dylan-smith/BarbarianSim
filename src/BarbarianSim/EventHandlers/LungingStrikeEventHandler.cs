using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class LungingStrikeEventHandler : EventHandler<LungingStrikeEvent>
{
    public LungingStrikeEventHandler(AttackSpeedCalculator attackSpeedCalculator, LungingStrike lungingStrike)
    {
        _attackSpeedCalculator = attackSpeedCalculator;
        _lungingStrike = lungingStrike;
    }

    private readonly AttackSpeedCalculator _attackSpeedCalculator;
    private readonly LungingStrike _lungingStrike;

    public override void ProcessEvent(LungingStrikeEvent e, SimulationState state)
    {
        e.FuryGeneratedEvent = new FuryGeneratedEvent(e.Timestamp, "Lunging Strike", LungingStrike.FURY_GENERATED);
        state.Events.Add(e.FuryGeneratedEvent);

        var weaponDamage = (state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike].MinDamage + state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike].MaxDamage) / 2.0;
        var skillMultiplier = _lungingStrike.GetSkillMultiplier(state);
        e.BaseDamage = weaponDamage * skillMultiplier;

        e.DirectDamageEvent = new DirectDamageEvent(e.Timestamp, "Lunging Strike", e.BaseDamage, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, LungingStrike.LUCKY_HIT_CHANCE, state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike], e.Target);
        state.Events.Add(e.DirectDamageEvent);

        var weaponSpeed = 1 / state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike].AttacksPerSecond;
        weaponSpeed *= _attackSpeedCalculator.Calculate(state);
        e.WeaponCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Lunging Strike", weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(e.WeaponCooldownAuraAppliedEvent);
    }
}
