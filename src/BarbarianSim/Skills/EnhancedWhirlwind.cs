using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class EnhancedWhirlwind : IHandlesEvent<DamageEvent>
{
    // Gain 1 Fury each time Whirlwind deals damage to an enemy, 4 Fury against Elite enemies
    public const double FURY_GAINED = 1;
    public const double ELITE_FURY_GAINED = 4;

    public EnhancedWhirlwind(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(DamageEvent e, SimulationState state)
    {
        if (e.DamageSource == DamageSource.Whirlwind && state.Config.Skills.ContainsKey(Skill.EnhancedWhirlwind))
        {
            var furyGenerated = state.Config.EnemySettings.IsElite ? ELITE_FURY_GAINED : FURY_GAINED;

            var furyGeneratedEvent = new FuryGeneratedEvent(e.Timestamp, "Enhanced Whirlwind", furyGenerated);
            state.Events.Add(furyGeneratedEvent);
            _log.Verbose($"Enhanced Whirlwind created FuryGeneratedEvent for {furyGenerated:F2} Fury");
        }
    }
}
