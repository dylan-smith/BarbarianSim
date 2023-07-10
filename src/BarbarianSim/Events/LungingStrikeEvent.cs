using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class LungingStrikeEvent : EventInfo
{
    public LungingStrikeEvent(double timestamp) : base(timestamp)
    { }

    public DamageEvent DamageEvent { get; set; }
    public FuryGeneratedEvent FuryGeneratedEvent { get; set; }
    public HealingEvent HealingEvent { get; set; }
    public AuraAppliedEvent BerserkingAuraAppliedEvent { get; set; }
    public BleedAppliedEvent BleedAppliedEvent { get; set; }
    public LuckyHitEvent LuckyHitEvent { get; set; }
    public AuraExpiredEvent WeaponCooldownCompletedEvent { get; set; }

    private const double FURY_GENERATED = 10.0;

    public override void ProcessEvent(SimulationState state)
    {
        var target = state.Enemies.First();

        var weaponDamage = (LungingStrike.Weapon.MinDamage + LungingStrike.Weapon.MaxDamage) / 2.0;
        var skillMultiplier = LungingStrike.GetSkillMultiplier(state);
        var damageMultiplier = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, target, SkillType.Basic);

        if (state.Config.Skills.ContainsKey(Skill.EnhancedLungingStrike) && target.IsHealthy())
        {
            damageMultiplier *= 1.3;
            HealingEvent = new HealingEvent(Timestamp, MaxLifeCalculator.Calculate(state) * 0.02);
            state.Events.Add(HealingEvent);
        }

        var damage = weaponDamage * skillMultiplier * damageMultiplier;
        var damageType = DamageType.Direct;

        if (state.Config.Skills.ContainsKey(Skill.BattleLungingStrike))
        {
            var bleedDamage = damage * 0.2;
            BleedAppliedEvent = new BleedAppliedEvent(Timestamp, bleedDamage, 5.0, target);
            state.Events.Add(BleedAppliedEvent);
        }

        var critChance = CritChanceCalculator.Calculate(state, DamageType.Physical);
        var critRoll = RandomGenerator.Roll(RollType.CriticalStrike);

        if (critRoll <= critChance)
        {
            damage *= CritDamageCalculator.Calculate(state, LungingStrike.Weapon.Expertise);
            damageType = DamageType.DirectCrit;

            if (state.Config.Skills.ContainsKey(Skill.CombatLungingStrike))
            {
                BerserkingAuraAppliedEvent = new AuraAppliedEvent(Timestamp, 1.5, Aura.Berserking);
                state.Events.Add(BerserkingAuraAppliedEvent);
            }
        }

        DamageEvent = new DamageEvent(Timestamp, damage, damageType, DamageSource.LungingStrike, SkillType.Basic, target);
        state.Events.Add(DamageEvent);

        FuryGeneratedEvent = new FuryGeneratedEvent(Timestamp, FURY_GENERATED);
        state.Events.Add(FuryGeneratedEvent);

        var weaponSpeed = 1 / LungingStrike.Weapon.AttacksPerSecond;
        weaponSpeed *= AttackSpeedCalculator.Calculate(state);
        state.Player.Auras.Add(Aura.WeaponCooldown);
        WeaponCooldownCompletedEvent = new AuraExpiredEvent(Timestamp + weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(WeaponCooldownCompletedEvent);

        var luckyRoll = RandomGenerator.Roll(RollType.LuckyHit);

        if (luckyRoll <= (LungingStrike.LUCKY_HIT_CHANCE + LuckyHitChanceCalculator.Calculate(state)))
        {
            LuckyHitEvent = new LuckyHitEvent(Timestamp, SkillType.Basic, target);
            state.Events.Add(LuckyHitEvent);
        }
    }
}
