using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class WhirlwindSpinEvent : EventInfo
{
    // Rapidly attack surrounding enemies for 17%[x] damage (Fury Cost: 11, Lucky Hit: 20%)
    public WhirlwindSpinEvent(double timestamp) : base(timestamp)
    { }

    public AuraAppliedEvent WhirlwindingAuraAppliedEvent { get; set; }
    public IList<DamageEvent> DamageEvents { get; init; } = new List<DamageEvent>();
    public IList<FuryGeneratedEvent> FuryGeneratedEvents { get; init; } = new List<FuryGeneratedEvent>();
    public FurySpentEvent FurySpentEvent { get; set; }
    public WhirlwindRefreshEvent WhirlwindRefreshEvent { get; set; }
    public IList<LuckyHitEvent> LuckyHitEvents { get; init; } = new List<LuckyHitEvent>();
    public AuraAppliedEvent WeaponCooldownAuraAppliedEvent { get; set; }
    public double BaseDamage { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        WhirlwindingAuraAppliedEvent = new AuraAppliedEvent(Timestamp, 0, Aura.Whirlwinding);
        state.Events.Add(WhirlwindingAuraAppliedEvent);

        FurySpentEvent = new FurySpentEvent(Timestamp, Whirlwind.FURY_COST, SkillType.Core);
        state.Events.Add(FurySpentEvent);

        var weaponDamage = (Whirlwind.Weapon.MinDamage + Whirlwind.Weapon.MaxDamage) / 2.0;
        var skillMultiplier = Whirlwind.GetSkillMultiplier(state);
        BaseDamage = weaponDamage * skillMultiplier;

        var critChance = CritChanceCalculator.Calculate(state, DamageType.Physical);

        foreach (var enemy in state.Enemies)
        {
            var damageMultiplier = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, enemy, SkillType.Core, DamageSource.Whirlwind);

            var damage = BaseDamage * damageMultiplier;

            var damageType = DamageType.Direct;
            var critRoll = RandomGenerator.Roll(RollType.CriticalStrike);

            if (critRoll <= critChance)
            {
                damage *= CritDamageCalculator.Calculate(state, Whirlwind.Weapon.Expertise);
                damageType = DamageType.DirectCrit;
            }

            var damageEvent = new DamageEvent(Timestamp, damage, damageType, DamageSource.Whirlwind, SkillType.Core, enemy);
            DamageEvents.Add(damageEvent);
            state.Events.Add(damageEvent);

            var luckyRoll = RandomGenerator.Roll(RollType.LuckyHit);

            if (luckyRoll <= (Whirlwind.LUCKY_HIT_CHANCE + LuckyHitChanceCalculator.Calculate(state)))
            {
                var luckyHitEvent = new LuckyHitEvent(Timestamp, SkillType.Core, enemy);
                LuckyHitEvents.Add(luckyHitEvent);
                state.Events.Add(luckyHitEvent);
            }
        }

        var weaponSpeed = 1 / Whirlwind.Weapon.AttacksPerSecond;
        weaponSpeed *= AttackSpeedCalculator.Calculate(state);
        WeaponCooldownAuraAppliedEvent = new AuraAppliedEvent(Timestamp, weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(WeaponCooldownAuraAppliedEvent);

        WhirlwindRefreshEvent = new WhirlwindRefreshEvent(Timestamp + weaponSpeed);
        state.Events.Add(WhirlwindRefreshEvent);
    }
}
