using BarbarianSim.GearSets;
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
        public SimulationConfig Config { get; set; } = new SimulationConfig();
        public readonly IList<string> Warnings = new List<string>();
        public readonly IList<string> Errors = new List<string>();

        public IEnumerable<DamageEvent> DamageEvents => ProcessedEvents.Where(e => e is DamageEvent).Cast<DamageEvent>();

        public bool Validate()
        {
            ApplyMetaGemBonuses();
            ApplyGearSetBonuses();

            var (warnings, errors) = Config.Validate();

            Warnings.AddRange(warnings);
            Errors.AddRange(errors);

            return !Errors.Any();
        }

        public void ApplyMetaGemBonuses()
        {
            var meta = Config.Gear.GetAllGems().Where(g => g.Color == GemColor.Meta).Cast<MetaGem>().ToList();

            meta.ForEach(m => m.Apply(this));
        }

        public void ApplyGearSetBonuses()
        {
            var gearSetTypes = typeof(IGearSet).Assembly.GetTypes().Where(t => t.IsClass && typeof(IGearSet).IsAssignableFrom(t)).ToList();

            foreach (var gearSetType in gearSetTypes)
            {
                var gearSet = (IGearSet)Activator.CreateInstance(gearSetType);

                gearSet.Apply(this);
            }
        }
    }
}
