using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class WhirlwindSpinEventHandler : EventHandler<WhirlwindSpinEvent>
{
    public WhirlwindSpinEventHandler(Whirlwind whirlwind, SimLogger log)
    {
        _whirlwind = whirlwind;
        _log = log;
    }

    private readonly Whirlwind _whirlwind;
    private readonly SimLogger _log;

    // Rapidly attack surrounding enemies for 17%[x] damage (Fury Cost: 11, Lucky Hit: 20%)
    public override void ProcessEvent(WhirlwindSpinEvent e, SimulationState state)
    {
        e.WhirlwindingAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Whirlwind", 0, Aura.Whirlwinding);
        state.Events.Add(e.WhirlwindingAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Whirlwinding");

        e.FurySpentEvent = new FurySpentEvent(e.Timestamp, "Whirlwind", Whirlwind.FURY_COST, SkillType.Core);
        state.Events.Add(e.FurySpentEvent);
        _log.Verbose($"Created FurySpentEvent for {Whirlwind.FURY_COST:F2} Fury");

        var weaponDamage = (state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind].MinDamage + state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind].MaxDamage) / 2.0;
        _log.Verbose($"Weapon Damage = {weaponDamage:F2}");
        var skillMultiplier = _whirlwind.GetSkillMultiplier(state);
        e.BaseDamage = weaponDamage * skillMultiplier;
        _log.Verbose($"Base Damage = {e.BaseDamage:F2}");

        foreach (var enemy in state.Enemies)
        {
            var directDamageEvent = new DirectDamageEvent(e.Timestamp, "Whirlwind", e.BaseDamage, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, Whirlwind.LUCKY_HIT_CHANCE, state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind], enemy);
            e.DirectDamageEvents.Add(directDamageEvent);
            state.Events.Add(directDamageEvent);
            _log.Verbose($"Created DirectDamageEvent for {e.BaseDamage:F2} damage on Enemy #{enemy.Id}");
        }

        var weaponSpeed = 1 / state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind].AttacksPerSecond;
        _log.Verbose($"Weapon Speed = {weaponSpeed:F2}");
        e.WeaponCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, "Whirlwind", weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(e.WeaponCooldownAuraAppliedEvent);
        _log.Verbose($"Created AuraAppliedEvent for Weapon Cooldown for {weaponSpeed:F2} seconds");

        e.WhirlwindRefreshEvent = new WhirlwindRefreshEvent(e.Timestamp + weaponSpeed);
        state.Events.Add(e.WhirlwindRefreshEvent);
        _log.Verbose($"Created WhirlwindRefreshEvent for timestamp {e.WhirlwindRefreshEvent.Timestamp:F2}");
    }
}
