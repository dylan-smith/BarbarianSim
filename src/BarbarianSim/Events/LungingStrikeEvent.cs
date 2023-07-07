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
    public BerserkingAppliedEvent BerserkingAppliedEvent { get; set; }
    public BleedAppliedEvent BleedAppliedEvent { get; set; }
    public LuckyHitEvent LuckyHitEvent { get; set; }

    private const double FURY_GENERATED = 10.0;

    public override void ProcessEvent(SimulationState state)
    {
        var target = state.Enemies.First();

        var weaponDamage = (LungingStrike.Weapon.MinDamage + LungingStrike.Weapon.MaxDamage) / 2.0;
        var skillMultiplier = LungingStrike.GetSkillMultiplier(state);
        var damageMultiplier = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, target);

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
            damage *= CritDamageCalculator.Calculate(state);
            damageType = DamageType.DirectCrit;

            if (state.Config.Skills.ContainsKey(Skill.CombatLungingStrike))
            {
                BerserkingAppliedEvent = new BerserkingAppliedEvent(Timestamp, 1.5);
                state.Events.Add(BerserkingAppliedEvent);
            }
        }

        DamageEvent = new DamageEvent(Timestamp, damage, damageType, DamageSource.LungingStrike, target);
        state.Events.Add(DamageEvent);

        FuryGeneratedEvent = new FuryGeneratedEvent(Timestamp, FURY_GENERATED);
        state.Events.Add(FuryGeneratedEvent);

        var weaponSpeed = 1 / LungingStrike.Weapon.AttacksPerSecond;
        weaponSpeed *= AttackSpeedCalculator.Calculate(state);
        state.Player.Auras.Add(Aura.WeaponCooldown);
        state.Events.Add(new WeaponAuraCooldownCompletedEvent(Timestamp + weaponSpeed));

        var luckyRoll = RandomGenerator.Roll(RollType.LuckyHit);

        if (luckyRoll <= (LungingStrike.LUCKY_HIT_CHANCE + LuckyHitChanceCalculator.Calculate(state)))
        {
            LuckyHitEvent = new LuckyHitEvent(Timestamp, SkillType.Basic, target);
            state.Events.Add(LuckyHitEvent);
        }
    }
}
