using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class TotalDamageMultiplierCalculator
{
    public TotalDamageMultiplierCalculator(AdditiveDamageBonusCalculator additiveDamageBonusCalculator,
                                           VulnerableDamageBonusCalculator vulnerableDamageBonusCalculator,
                                           StrengthCalculator strengthCalculator,
                                           PitFighter pitFighter,
                                           WarCry warCry,
                                           SupremeWrathOfTheBerserker supremeWrathOfTheBerserker,
                                           UnbridledRage unbridledRage,
                                           ViolentWhirlwind violentWhirlwind,
                                           EnhancedLungingStrike enhancedLungingStrike,
                                           EdgemastersAspect edgemastersAspect,
                                           AspectOfLimitlessRage aspectOfLimitlessRage,
                                           ConceitedAspect conceitedAspect)
    {
        _additiveDamageBonusCalculator = additiveDamageBonusCalculator;
        _vulnerableDamageBonusCalculator = vulnerableDamageBonusCalculator;
        _strengthCalculator = strengthCalculator;
        _pitFighter = pitFighter;
        _warCry = warCry;
        _supremeWrathOfTheBerserker = supremeWrathOfTheBerserker;
        _unbridledRage = unbridledRage;
        _violentWhirlwind = violentWhirlwind;
        _enhancedLungingStrike = enhancedLungingStrike;
        _edgemastersAspect = edgemastersAspect;
        _aspectOfLimitlessRage = aspectOfLimitlessRage;
        _conceitedAspect = conceitedAspect;
    }

    private readonly AdditiveDamageBonusCalculator _additiveDamageBonusCalculator;
    private readonly VulnerableDamageBonusCalculator _vulnerableDamageBonusCalculator;
    private readonly StrengthCalculator _strengthCalculator;
    private readonly PitFighter _pitFighter;
    private readonly WarCry _warCry;
    private readonly SupremeWrathOfTheBerserker _supremeWrathOfTheBerserker;
    private readonly UnbridledRage _unbridledRage;
    private readonly ViolentWhirlwind _violentWhirlwind;
    private readonly EnhancedLungingStrike _enhancedLungingStrike;
    private readonly EdgemastersAspect _edgemastersAspect;
    private readonly AspectOfLimitlessRage _aspectOfLimitlessRage;
    private readonly ConceitedAspect _conceitedAspect;

    public virtual double Calculate(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType, DamageSource damageSource)
    {
        var damageBonus = _additiveDamageBonusCalculator.Calculate(state, damageType, enemy);
        damageBonus *= _vulnerableDamageBonusCalculator.Calculate(state, enemy);
        damageBonus *= 1 + (_strengthCalculator.Calculate(state) * 0.001);
        damageBonus *= _pitFighter.GetCloseDamageBonus(state);
        damageBonus *= _warCry.GetDamageBonus(state);
        damageBonus *= _unbridledRage.GetDamageBonus(state, skillType);
        damageBonus *= _violentWhirlwind.GetDamageBonus(state, damageSource);
        damageBonus *= _enhancedLungingStrike.GetDamageBonus(state, damageSource, enemy);
        damageBonus *= _supremeWrathOfTheBerserker.GetBerserkDamageBonus(state);
        damageBonus *= _edgemastersAspect.GetDamageBonus(state, skillType);
        damageBonus *= _aspectOfLimitlessRage.GetDamageBonus(state, skillType);
        damageBonus *= _conceitedAspect.GetDamageBonus(state);

        return damageBonus;
    }
}
