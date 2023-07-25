using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.EventHandlers;

public class WhirlwindSpinEventHandler : EventHandler<WhirlwindSpinEvent>
{
    public WhirlwindSpinEventHandler(Whirlwind whirlwind) => _whirlwind = whirlwind;

    private readonly Whirlwind _whirlwind;

    // Rapidly attack surrounding enemies for 17%[x] damage (Fury Cost: 11, Lucky Hit: 20%)
    public override void ProcessEvent(WhirlwindSpinEvent e, SimulationState state)
    {
        e.WhirlwindingAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, 0, Aura.Whirlwinding);
        state.Events.Add(e.WhirlwindingAuraAppliedEvent);

        e.FurySpentEvent = new FurySpentEvent(e.Timestamp, Whirlwind.FURY_COST, SkillType.Core);
        state.Events.Add(e.FurySpentEvent);

        var weaponDamage = (state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind].MinDamage + state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind].MaxDamage) / 2.0;
        var skillMultiplier = _whirlwind.GetSkillMultiplier(state);
        e.BaseDamage = weaponDamage * skillMultiplier;

        foreach (var enemy in state.Enemies)
        {
            var directDamageEvent = new DirectDamageEvent(e.Timestamp, e.BaseDamage, DamageType.Physical, DamageSource.Whirlwind, SkillType.Core, Whirlwind.LUCKY_HIT_CHANCE, state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind], enemy);
            e.DirectDamageEvents.Add(directDamageEvent);
            state.Events.Add(directDamageEvent);
        }

        var weaponSpeed = 1 / state.Config.PlayerSettings.SkillWeapons[Skill.Whirlwind].AttacksPerSecond;
        e.WeaponCooldownAuraAppliedEvent = new AuraAppliedEvent(e.Timestamp, weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(e.WeaponCooldownAuraAppliedEvent);

        e.WhirlwindRefreshEvent = new WhirlwindRefreshEvent(e.Timestamp + weaponSpeed);
        state.Events.Add(e.WhirlwindRefreshEvent);
    }
}
