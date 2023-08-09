using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Paragon;

public class Wrath : IHandlesEvent<DamageEvent>
{
    // Skills that Critically Strike generate 3 Fury.
    public const double FURY_GENERATED = 3;

    public Wrath(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(DamageEvent e, SimulationState state)
    {
        if (!state.Config.HasParagonNode(ParagonNode.Wrath))
        {
            return;
        }

        if (e.SkillType != SkillType.None && e.DamageType.HasFlag(DamageType.CriticalStrike))
        {
            state.Events.Add(new FuryGeneratedEvent(e.Timestamp, "Wrath", FURY_GENERATED));
            _log.Verbose($"Wrath created FuryGeneratedEvent for {FURY_GENERATED:F2} Fury");
        }
    }
}
