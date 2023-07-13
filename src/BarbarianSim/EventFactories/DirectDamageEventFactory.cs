using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventFactories;

public class DirectDamageEventFactory
{
    public DirectDamageEventFactory(TotalDamageMultiplierCalculator totalDamageMultiplierCalculator,
                                    CritChanceCalculator critChanceCalculator,
                                    RandomGenerator randomGenerator,
                                    CritDamageCalculator critDamageCalculator,
                                    DamageEventFactory damageEventFactory,
                                    LuckyHitChanceCalculator luckyHitChanceCalculator,
                                    LuckyHitEventFactory luckyHitEventFactory)
    {
        _totalDamageMultiplierCalculator = totalDamageMultiplierCalculator;
        _critChanceCalculator = critChanceCalculator;
        _randomGenerator = randomGenerator;
        _critDamageCalculator = critDamageCalculator;
        _damageEventFactory = damageEventFactory;
        _luckyHitChanceCalculator = luckyHitChanceCalculator;
        _luckyHitEventFactory = luckyHitEventFactory;
    }

    private readonly TotalDamageMultiplierCalculator _totalDamageMultiplierCalculator;
    private readonly CritChanceCalculator _critChanceCalculator;
    private readonly RandomGenerator _randomGenerator;
    private readonly CritDamageCalculator _critDamageCalculator;
    private readonly DamageEventFactory _damageEventFactory;
    private readonly LuckyHitChanceCalculator _luckyHitChanceCalculator;
    private readonly LuckyHitEventFactory _luckyHitEventFactory;

    public DirectDamageEvent Create(double timestamp, double baseDamage, DamageType damageType, DamageSource damageSource, SkillType skillType, double luckyHitChance, Expertise expertise, EnemyState enemy)
        => new(_totalDamageMultiplierCalculator,
               _critChanceCalculator,
               _randomGenerator,
               _critDamageCalculator,
               _damageEventFactory,
               _luckyHitChanceCalculator,
               _luckyHitEventFactory,
               timestamp,
               baseDamage,
               damageType,
               damageSource,
               skillType,
               luckyHitChance,
               expertise,
               enemy);
}
