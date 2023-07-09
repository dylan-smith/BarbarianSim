using BarbarianSim.Abilities;
using BarbarianSim.Enums;
using BarbarianSim.Skills;

namespace BarbarianSim.StatCalculators;

public class TotalDamageMultiplierCalculator : BaseStatCalculator
{
    public static double Calculate(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType) => Calculate<TotalDamageMultiplierCalculator>(state, damageType, enemy, skillType);

    protected override double InstanceCalculate(SimulationState state, DamageType damageType, EnemyState enemy, SkillType skillType)
    {
        var damageBonus = AdditiveDamageBonusCalculator.Calculate(state, damageType, enemy);
        damageBonus *= VulnerableDamageBonusCalculator.Calculate(state, enemy);
        damageBonus *= 1 + (StrengthCalculator.Calculate(state) * 0.001);
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

        damageBonus *= WrathOfTheBerserker.GetBerserkDamageBonus(state);

        return damageBonus;
    }
}
