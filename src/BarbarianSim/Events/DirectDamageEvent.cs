using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class DirectDamageEvent : EventInfo
{
    public DirectDamageEvent(TotalDamageMultiplierCalculator totalDamageMultiplierCalculator,
                             CritChanceCalculator critChanceCalculator,
                             RandomGenerator randomGenerator,
                             CritDamageCalculator critDamageCalculator,
                             DamageEventFactory damageEventFactory,
                             LuckyHitChanceCalculator luckyHitChanceCalculator,
                             LuckyHitEventFactory luckyHitEventFactory,
                             double timestamp,
                             double baseDamage,
                             DamageType damageType,
                             DamageSource damageSource,
                             SkillType skillType,
                             double luckyHitChance,
                             Expertise expertise,
                             EnemyState enemy) : base(timestamp)
    {
        _totalDamageMultiplierCalculator = totalDamageMultiplierCalculator;
        _critChanceCalculator = critChanceCalculator;
        _randomGenerator = randomGenerator;
        _critDamageCalculator = critDamageCalculator;
        _damageEventFactory = damageEventFactory;
        _luckyHitChanceCalculator = luckyHitChanceCalculator;
        _luckyHitEventFactory = luckyHitEventFactory;
        BaseDamage = baseDamage;
        DamageType = damageType;
        DamageSource = damageSource;
        SkillType = skillType;
        LuckyHitChance = luckyHitChance;
        Expertise = expertise;
        Enemy = enemy;
    }

    private readonly TotalDamageMultiplierCalculator _totalDamageMultiplierCalculator;
    private readonly CritChanceCalculator _critChanceCalculator;
    private readonly RandomGenerator _randomGenerator;
    private readonly CritDamageCalculator _critDamageCalculator;
    private readonly DamageEventFactory _damageEventFactory;
    private readonly LuckyHitChanceCalculator _luckyHitChanceCalculator;
    private readonly LuckyHitEventFactory _luckyHitEventFactory;

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
        var damageMultiplier = _totalDamageMultiplierCalculator.Calculate(state, DamageType, Enemy, SkillType, DamageSource);

        var damage = BaseDamage * damageMultiplier;

        var critChance = _critChanceCalculator.Calculate(state, DamageType);
        var critRoll = _randomGenerator.Roll(RollType.CriticalStrike);

        var damageType = DamageType | DamageType.Direct;

        if (critRoll <= critChance)
        {
            damage *= _critDamageCalculator.Calculate(state, Expertise);
            damageType |= DamageType.CriticalStrike;
        }

        DamageEvent = _damageEventFactory.Create(Timestamp, damage, damageType, DamageSource, SkillType, Enemy);
        state.Events.Add(DamageEvent);

        var luckyRoll = _randomGenerator.Roll(RollType.LuckyHit);

        if (luckyRoll <= (LuckyHitChance + _luckyHitChanceCalculator.Calculate(state)))
        {
            LuckyHitEvent = _luckyHitEventFactory.Create(Timestamp, SkillType, Enemy);
            state.Events.Add(LuckyHitEvent);
        }
    }
}
