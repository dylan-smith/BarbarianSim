using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class BattleLungingStrike : IHandlesEvent<LungingStrikeEvent>
{
    // Lunging strike also inflicts 20% bleeding damage over 5 seconds
    public const double BLEED_DAMAGE = 0.2;
    public const double BLEED_DURATION = 5;

    public void ProcessEvent(LungingStrikeEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.BattleLungingStrike))
        {
            var bleedAppliedEvent = new BleedAppliedEvent(e.Timestamp, e.BaseDamage * BLEED_DAMAGE, BLEED_DURATION, e.Target);
            state.Events.Add(bleedAppliedEvent);
        }
    }
}
