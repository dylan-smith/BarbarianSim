﻿using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim;

public class SimulationState
{
    public IList<EventInfo> Events { get; init; } = new List<EventInfo>();
    public IList<EventInfo> ProcessedEvents { get; init; } = new List<EventInfo>();
    public double CurrentTime { get; set; }
    public ISet<Aura> Auras { get; init; } = new HashSet<Aura>();
    public SimulationConfig Config { get; init; }
    public IList<string> Warnings { get; init; } = new List<string>();
    public IList<string> Errors { get; init; } = new List<string>();
    public EnemyState Enemy { get; init; } = new();

    public SimulationState(SimulationConfig config)
    {
        Config = config;
        Enemy.MaxLife = config.EnemySettings.Life;
        Enemy.Life = Enemy.MaxLife;
    }

    public IEnumerable<DamageEvent> DamageEvents => ProcessedEvents.Where(e => e is DamageEvent).Cast<DamageEvent>();

    public virtual bool Validate()
    {
        var (warnings, errors) = Config.Validate();

        Warnings.AddRange(warnings);
        Errors.AddRange(errors);

        return !Errors.Any();
    }
}
