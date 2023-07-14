using BarbarianSim.Abilities;
using BarbarianSim.Aspects;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class TotalDamageMultiplierCalculator
{
    public TotalDamageMultiplierCalculator(AdditiveDamageBonusCalculator additiveDamageBonusCalculator,
                                           VulnerableDamageBonusCalculator vulnerableDamageBonusCalculator,
                                           StrengthCalculator strengthCalculator)
    {
        _additiveDamageBonusCalculator = additiveDamageBonusCalculator;
        _vulnerableDamageBonusCalculator = vulnerableDamageBonusCalculator;
        _strengthCalculator = strengthCalculator;
    }

    private readonly AdditiveDamageBonusCalculator _additiveDamageBonusCalculator;
    private readonly VulnerableDamageBonusCalculator _vulnerableDamageBonusCalculator;
    private readonly StrengthCalculator _strengthCalculator;

    public double Calculate(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType, DamageSource damageSource)
    {
        var damageBonus = _additiveDamageBonusCalculator.Calculate(state, damageType, enemy);
        damageBonus *= _vulnerableDamageBonusCalculator.Calculate(state, enemy);
        damageBonus *= 1 + (_strengthCalculator.Calculate(state) * 0.001);
        damageBonus *= PitFighter.GetCloseDamageBonus(state);

        if (state.Player.Auras.Contains(Aura.WarCry))
        {
            damageBonus *= WarCry.GetDamageBonus(state);
        }

        if (state.Config.Skills.ContainsKey(Skill.UnbridledRage))
        {
            if (skillType == SkillType.Core)
            {
                damageBonus *= 2;
            }
        }

        if (damageSource == DamageSource.Whirlwind && state.Player.Auras.Contains(Aura.ViolentWhirlwind))
        {
            damageBonus *= ViolentWhirlwind.DAMAGE_MULTIPLIER;
        }

        if (damageSource == DamageSource.LungingStrike &&
            state.Config.Skills.ContainsKey(Skill.EnhancedLungingStrike) &&
            enemy.IsHealthy())
        {
            damageBonus *= EnhancedLungingStrike.DAMAGE_MULTIPLIER;
        }

        damageBonus *= WrathOfTheBerserker.GetBerserkDamageBonus(state);

        var edgemasters = state.Config.Gear.GetAspect<EdgemastersAspect>();
        if (edgemasters != null)
        {
            damageBonus *= edgemasters.GetDamageBonus(state, skillType);
        }

        return damageBonus;
    }
}
