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
                                           ViolentWhirlwind violentWhirlwind)
    {
        _additiveDamageBonusCalculator = additiveDamageBonusCalculator;
        _vulnerableDamageBonusCalculator = vulnerableDamageBonusCalculator;
        _strengthCalculator = strengthCalculator;
        _pitFighter = pitFighter;
        _warCry = warCry;
        _supremeWrathOfTheBerserker = supremeWrathOfTheBerserker;
        _unbridledRage = unbridledRage;
        _violentWhirlwind = violentWhirlwind;
    }

    private readonly AdditiveDamageBonusCalculator _additiveDamageBonusCalculator;
    private readonly VulnerableDamageBonusCalculator _vulnerableDamageBonusCalculator;
    private readonly StrengthCalculator _strengthCalculator;
    private readonly PitFighter _pitFighter;
    private readonly WarCry _warCry;
    private readonly SupremeWrathOfTheBerserker _supremeWrathOfTheBerserker;
    private readonly UnbridledRage _unbridledRage;
    private readonly ViolentWhirlwind _violentWhirlwind;

    public virtual double Calculate(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType, DamageSource damageSource)
    {
        var damageBonus = _additiveDamageBonusCalculator.Calculate(state, damageType, enemy);
        damageBonus *= _vulnerableDamageBonusCalculator.Calculate(state, enemy);
        damageBonus *= 1 + (_strengthCalculator.Calculate(state) * 0.001);
        damageBonus *= _pitFighter.GetCloseDamageBonus(state);
        damageBonus *= _warCry.GetDamageBonus(state);
        damageBonus *= _unbridledRage.GetDamageBonus(state, skillType);
        damageBonus *= _violentWhirlwind.GetDamageBonus(state, damageSource);

        if (damageSource == DamageSource.LungingStrike &&
            state.Config.Skills.ContainsKey(Skill.EnhancedLungingStrike) &&
            enemy.IsHealthy())
        {
            damageBonus *= EnhancedLungingStrike.DAMAGE_MULTIPLIER;
        }

        damageBonus *= _supremeWrathOfTheBerserker.GetBerserkDamageBonus(state);

        var edgemasters = state.Config.Gear.GetAspect<EdgemastersAspect>();
        if (edgemasters != null)
        {
            damageBonus *= edgemasters.GetDamageBonus(state, skillType);
        }

        return damageBonus;
    }
}
