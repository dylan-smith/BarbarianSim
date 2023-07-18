using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class CombatLungingStrike : IHandlesEvent<DamageEvent>
{
    // Critical strikes with Lunging Strike grant you Berserking for 1.5 seconds
    public const double BERSERKING_DURATION = 1.5;

    public void ProcessEvent(DamageEvent damageEvent, SimulationState state)
    {
        if (damageEvent.DamageSource == DamageSource.LungingStrike &&
            state.Config.Skills.ContainsKey(Skill.CombatLungingStrike) &&
            damageEvent.DamageType.HasFlag(DamageType.CriticalStrike))
        {
            state.Events.Add(new AuraAppliedEvent(damageEvent.Timestamp, BERSERKING_DURATION, Aura.Berserking));
        }
    }
}
