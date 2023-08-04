using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Skills;

public class EnhancedLungingStrike : IHandlesEvent<LungingStrikeEvent>
{
    // Lunging strike deals 30%[x] increased damage and Heals you for 2% Maximum Life when it damages a Healthy enemy
    public const double DAMAGE_MULTIPLIER = 1.3;
    public const double HEAL_PERCENT = 0.02;

    public EnhancedLungingStrike(MaxLifeCalculator maxLifeCalculator) => _maxLifeCalculator = maxLifeCalculator;

    private readonly MaxLifeCalculator _maxLifeCalculator;

    public void ProcessEvent(LungingStrikeEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.EnhancedLungingStrike) &&
            e.Target.IsHealthy())
        {
            var healingEvent = new HealingEvent(e.Timestamp, "Enhanced Lunging Strike", _maxLifeCalculator.Calculate(state) * HEAL_PERCENT);
            state.Events.Add(healingEvent);
        }
    }

    public virtual double GetDamageBonus(SimulationState state, DamageSource damageSource, EnemyState enemy)
    {
        return damageSource == DamageSource.LungingStrike &&
            state.Config.HasSkill(Skill.EnhancedLungingStrike) &&
            enemy.IsHealthy()
            ? EnhancedLungingStrike.DAMAGE_MULTIPLIER
            : 1.0;
    }
}
