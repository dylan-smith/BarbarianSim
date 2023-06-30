using BarbarianSim.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BarbarianSim
{
    public class SimulationState
    {
        public readonly IList<EventInfo> Events = new List<EventInfo>();
        public readonly IList<EventInfo> ProcessedEvents = new List<EventInfo>();
        public double CurrentTime = 0.0;
        public readonly ISet<Aura> Auras = new HashSet<Aura>();
        public readonly ISet<Aura> EnemyAuras = new HashSet<Aura>();
        public SimulationConfig Config { get; init; }
        public readonly IList<string> Warnings = new List<string>();
        public readonly IList<string> Errors = new List<string>();

        public int EnemyLife;

        public SimulationState(SimulationConfig config)
        {
            Config = config;
            EnemyLife = config.EnemySettings.Life;
        }

        public IEnumerable<DamageEvent> DamageEvents => ProcessedEvents.Where(e => e is DamageEvent).Cast<DamageEvent>();

        public bool Validate()
        {
            var (warnings, errors) = Config.Validate();

            Warnings.AddRange(warnings);
            Errors.AddRange(errors);

            return !Errors.Any();
        }
    }
}
