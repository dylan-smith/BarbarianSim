using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class WhirlwindStartedEvent : EventInfo
{
    public WhirlwindStartedEvent(double timestamp) : base(timestamp)
    { }

    public IList<DamageEvent> DamageEvents { get; init; } = new List<DamageEvent>();
    public IList<FuryGeneratedEvent> FuryGeneratedEvents { get; init; } = new List<FuryGeneratedEvent>();
    public IList<BleedAppliedEvent> BleedAppliedEvents { get; init; } = new List<BleedAppliedEvent>();
    public FurySpentEvent FurySpentEvent { get; set; }
    public WhirlwindRefreshEvent WhirlwindRefreshEvent { get; set; }
    public ViolentWhirlwindAppliedEvent ViolentWhirlwindAppliedEvent { get; set; }
    public IList<LuckyHitEvent> LuckyHitEvents { get; init; } = new List<LuckyHitEvent>();
    public CooldownCompletedEvent WeaponCooldownCompletedEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.Whirlwinding);

        FurySpentEvent = new FurySpentEvent(Timestamp, Whirlwind.FURY_COST, SkillType.Core);
        state.Events.Add(FurySpentEvent);

        var weaponDamage = (Whirlwind.Weapon.MinDamage + Whirlwind.Weapon.MaxDamage) / 2.0;
        var skillMultiplier = Whirlwind.GetSkillMultiplier(state);
        var violentWhirlwindMultiplier = state.Player.Auras.Contains(Aura.ViolentWhirlwind) ? 1.3 : 1.0;

        if (state.Config.Skills.ContainsKey(Skill.ViolentWhirlwind))
        {
            ViolentWhirlwindAppliedEvent = new ViolentWhirlwindAppliedEvent(Timestamp + 2);
            state.Events.Add(ViolentWhirlwindAppliedEvent);
        }

        var critChance = CritChanceCalculator.Calculate(state, DamageType.Physical);

        foreach (var enemy in state.Enemies)
        {
            var damageMultiplier = TotalDamageMultiplierCalculator.Calculate(state, DamageType.Physical, enemy, SkillType.Core);

            var damage = weaponDamage * skillMultiplier * damageMultiplier * violentWhirlwindMultiplier;

            var damageType = DamageType.Direct;
            var critRoll = RandomGenerator.Roll(RollType.CriticalStrike);

            if (critRoll <= critChance)
            {
                damage *= CritDamageCalculator.Calculate(state);
                damageType = DamageType.DirectCrit;
            }

            var damageEvent = new DamageEvent(Timestamp, damage, damageType, DamageSource.Whirlwind, enemy);
            DamageEvents.Add(damageEvent);
            state.Events.Add(damageEvent);

            if (state.Config.Skills.ContainsKey(Skill.EnhancedWhirlwind))
            {
                var furyGenerated = state.Config.EnemySettings.IsElite ? 4 : 1;

                var furyGeneratedEvent = new FuryGeneratedEvent(Timestamp, furyGenerated);
                FuryGeneratedEvents.Add(furyGeneratedEvent);
                state.Events.Add(furyGeneratedEvent);
            }

            if (state.Config.Skills.ContainsKey(Skill.FuriousWhirlwind) && Whirlwind.Weapon == state.Config.Gear.TwoHandSlashing)
            {
                var bleedAppliedEvent = new BleedAppliedEvent(Timestamp, weaponDamage * damageMultiplier * 0.4, 5, enemy);
                BleedAppliedEvents.Add(bleedAppliedEvent);
                state.Events.Add(bleedAppliedEvent);
            }

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
        state.Player.Auras.Add(Aura.WeaponCooldown);
        WeaponCooldownCompletedEvent = new CooldownCompletedEvent(Timestamp + weaponSpeed, Aura.WeaponCooldown);
        state.Events.Add(WeaponCooldownCompletedEvent);

        WhirlwindRefreshEvent = new WhirlwindRefreshEvent(Timestamp + weaponSpeed);
        state.Events.Add(WhirlwindRefreshEvent);
    }
}
