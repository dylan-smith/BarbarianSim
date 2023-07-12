using BarbarianSim.Enums;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class DirectDamageEvent : EventInfo
{
    public DirectDamageEvent(double timestamp, double baseDamage, DamageType damageType, DamageSource damageSource, SkillType skillType, double luckyHitChance, Expertise expertise, EnemyState enemy) : base(timestamp)
    {
        BaseDamage = baseDamage;
        DamageType = damageType;
        DamageSource = damageSource;
        SkillType = skillType;
        LuckyHitChance = luckyHitChance;
        Expertise = expertise;
        Enemy = enemy;
    }

    public double BaseDamage { get; init; }
    public DamageType DamageType { get; init; }
    public DamageSource DamageSource { get; init; }
    public SkillType SkillType { get; init; }
    public double LuckyHitChance { get; init; }
    public Expertise Expertise { get; init; }
    public EnemyState Enemy { get; init; }

    public DamageEvent DamageEvent { get; set; }
    public LuckyHitEvent LuckyHitEvent { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        var damageMultiplier = TotalDamageMultiplierCalculator.Calculate(state, DamageType, Enemy, SkillType, DamageSource);

        var damage = BaseDamage * damageMultiplier;

        var critChance = CritChanceCalculator.Calculate(state, DamageType);
        var critRoll = RandomGenerator.Roll(RollType.CriticalStrike);

        var damageType = DamageType.Direct;

        if (critRoll <= critChance)
        {
            damage *= CritDamageCalculator.Calculate(state, Expertise);
            damageType = DamageType.DirectCrit;
        }

        DamageEvent = new DamageEvent(Timestamp, damage, damageType, DamageSource.Whirlwind, SkillType.Core, Enemy);
        state.Events.Add(DamageEvent);

        var luckyRoll = RandomGenerator.Roll(RollType.LuckyHit);

        if (luckyRoll <= (LuckyHitChance + LuckyHitChanceCalculator.Calculate(state)))
        {
            LuckyHitEvent = new LuckyHitEvent(Timestamp, SkillType, Enemy);
            state.Events.Add(LuckyHitEvent);
        }
    }
}
