using BarbarianSim.Enums;
using BarbarianSim.EventFactories;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class BattleLungingStrike
{
    // Lunging strike also inflicts 20% bleeding damage over 5 seconds
    public const double BLEED_DAMAGE = 0.2;
    public const double BLEED_DURATION = 5;

    public BattleLungingStrike(BleedAppliedEventFactory bleedAppliedEventFactory) => _bleedAppliedEventFactory = bleedAppliedEventFactory;

    private readonly BleedAppliedEventFactory _bleedAppliedEventFactory;

    public void ProcessEvent(LungingStrikeEvent lungingStrikeEvent, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.BattleLungingStrike))
        {
            var bleedAppliedEvent = _bleedAppliedEventFactory.Create(lungingStrikeEvent.Timestamp, lungingStrikeEvent.BaseDamage * BLEED_DAMAGE, BLEED_DURATION, lungingStrikeEvent.Target);
            state.Events.Add(bleedAppliedEvent);
        }
    }
}
