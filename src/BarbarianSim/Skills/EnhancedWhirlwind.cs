using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class EnhancedWhirlwind
{
    // Gain 1 Fury each time Whirlwind deals damage to an enemy, 4 Fury against Elite enemies
    public const double FURY_GAINED = 1;
    public const double ELITE_FURY_GAINED = 4;

    public EnhancedWhirlwind(FuryGeneratedEventFactory furyGeneratedEventFactory) => _furyGeneratedEventFactory = furyGeneratedEventFactory;

    private readonly FuryGeneratedEventFactory _furyGeneratedEventFactory;

    public void ProcessEvent(DamageEvent damageEvent, SimulationState state)
    {
        if (damageEvent.DamageSource == DamageSource.Whirlwind && state.Config.Skills.ContainsKey(Skill.EnhancedWhirlwind))
        {
            var furyGenerated = state.Config.EnemySettings.IsElite ? ELITE_FURY_GAINED : FURY_GAINED;

            var furyGeneratedEvent = _furyGeneratedEventFactory.Create(damageEvent.Timestamp, furyGenerated);
            state.Events.Add(furyGeneratedEvent);
        }
    }
}
