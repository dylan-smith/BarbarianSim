using BarbarianSim.Enums;
using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.Skills;

public class EnhancedLungingStrike : IHandlesEvent<LungingStrikeEvent>
{
    // Lunging strike deals 30%[x] increased damage and Heals you for 2% Maximum Life when it damages a Healthy enemy
    public const double DAMAGE_MULTIPLIER = 1.3;
    public const double HEAL_PERCENT = 0.02;

    public EnhancedLungingStrike(MaxLifeCalculator maxLifeCalculator, SimLogger log)
    {
        _maxLifeCalculator = maxLifeCalculator;
        _log = log;
    }

    private readonly MaxLifeCalculator _maxLifeCalculator;
    private readonly SimLogger _log;

    public void ProcessEvent(LungingStrikeEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.EnhancedLungingStrike) &&
            e.Target.IsHealthy())
        {
            var healingAmount = _maxLifeCalculator.Calculate(state) * HEAL_PERCENT;
            var healingEvent = new HealingEvent(e.Timestamp, "Enhanced Lunging Strike", healingAmount);
            state.Events.Add(healingEvent);
            _log.Verbose($"Enhanced Lunging Strike created HealingEvent for {healingAmount:F2}");
        }
    }

    public virtual double GetDamageBonus(SimulationState state, DamageSource damageSource, EnemyState enemy)
    {
        if (damageSource == DamageSource.LungingStrike &&
            state.Config.HasSkill(Skill.EnhancedLungingStrike) &&
            enemy.IsHealthy())
        {
            _log.Verbose($"Damage Bonus from Enhanced Lunging Strike = {DAMAGE_MULTIPLIER:F2}x");
            return DAMAGE_MULTIPLIER;
        }

        return 1.0;
    }
}
