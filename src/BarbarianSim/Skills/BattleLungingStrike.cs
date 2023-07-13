using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public static class BattleLungingStrike
{
    // Lunging strike also inflicts 20% bleeding damage over 5 seconds
    public const double BLEED_DAMAGE = 0.2;
    public const double BLEED_DURATION = 5;

    public static void ProcessEvent(LungingStrikeEvent lungingStrikeEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.BattleLungingStrike))
        {
            var bleedAppliedEvent = new BleedAppliedEvent(lungingStrikeEvent.Timestamp, lungingStrikeEvent.BaseDamage * BLEED_DAMAGE, BLEED_DURATION, lungingStrikeEvent.Target);
            state.Events.Add(bleedAppliedEvent);
        }
    }
}
