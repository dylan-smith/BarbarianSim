using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Paragon;

public class Wrath : IHandlesEvent<DamageEvent>
{
    // Skills that Critically Strike generate 3 Fury.
    public const double FURY_GENERATED = 3;

    public void ProcessEvent(DamageEvent e, SimulationState state)
    {
        if (!state.Config.HasParagonNode(ParagonNode.Wrath))
        {
            return;
        }

        if (e.SkillType != SkillType.None && e.DamageType.HasFlag(DamageType.CriticalStrike))
        {
            state.Events.Add(new FuryGeneratedEvent(e.Timestamp, FURY_GENERATED));
        }
    }
}
