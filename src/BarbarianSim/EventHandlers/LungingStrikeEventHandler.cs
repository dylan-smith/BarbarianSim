using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class LungingStrikeEventHandler : EventHandler<LungingStrikeEvent>
{
    public LungingStrikeEventHandler(AttackSpeedCalculator attackSpeedCalculator, LungingStrike lungingStrike, SimLogger log)
    {
        _attackSpeedCalculator = attackSpeedCalculator;
        _lungingStrike = lungingStrike;
        _log = log;
    }

    private readonly AttackSpeedCalculator _attackSpeedCalculator;
    private readonly LungingStrike _lungingStrike;
    private readonly SimLogger _log;

    public override void ProcessEvent(LungingStrikeEvent e, SimulationState state)
    {
        e.FuryGeneratedEvent = new FuryGeneratedEvent(e.Timestamp, "Lunging Strike", LungingStrike.FURY_GENERATED);
        state.Events.Add(e.FuryGeneratedEvent);
        _log.Verbose($"Created FuryGeneratedEvent for {e.FuryGeneratedEvent.BaseFury} Fury");

        var weaponDamage = (state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike].MinDamage + state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike].MaxDamage) / 2.0;
        _log.Verbose($"Weapon Damage = {weaponDamage}");
        var skillMultiplier = _lungingStrike.GetSkillMultiplier(state);
        _log.Verbose($"Skill Multiplier = {skillMultiplier}");
        e.BaseDamage = weaponDamage * skillMultiplier;
        _log.Verbose($"Base Damage = {e.BaseDamage}");

        e.DirectDamageEvent = new DirectDamageEvent(e.Timestamp, "Lunging Strike", e.BaseDamage, DamageType.Physical, DamageSource.LungingStrike, SkillType.Basic, LungingStrike.LUCKY_HIT_CHANCE, state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike], e.Target);
        state.Events.Add(e.DirectDamageEvent);
        _log.Verbose($"Created DirectDamageEvent for {e.DirectDamageEvent.BaseDamage} damage");

        var weaponSpeed = 1 / state.Config.PlayerSettings.SkillWeapons[Skill.LungingStrike].AttacksPerSecond;
        _log.Verbose($"Weapon Speed = {weaponSpeed}");
        weaponSpeed *= _attackSpeedCalculator.Calculate(state);
        _log.Verbose($"Weapon Speed (after attack speed) = {weaponSpeed:F2}");
        e.WeaponCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Lunging Strike", weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(e.WeaponCooldownAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for WeaponCooldown for {weaponSpeed:F2} seconds");
    }
}
