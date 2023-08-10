using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Skills;

public class BattleLungingStrike : IHandlesEvent<LungingStrikeEvent>
{
    // Lunging strike also inflicts 20% bleeding damage over 5 seconds
    public const double BLEED_DAMAGE = 0.2;
    public const double BLEED_DURATION = 5;

    public BattleLungingStrike(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(LungingStrikeEvent e, SimulationState state)
    {
        if (state.Config.Skills.ContainsKey(Skill.BattleLungingStrike))
        {
            var bleedDamage = e.BaseDamage * BLEED_DAMAGE;
            var bleedAppliedEvent = new BleedAppliedEvent(e.Timestamp, "Battle Lunging Strike", bleedDamage, BLEED_DURATION, e.Target);
            state.Events.Add(bleedAppliedEvent);
            _log.Verbose($"Battle Lunging Strike created BleedAppliedEvent for {bleedDamage:F2} damage over {BLEED_DURATION} seconds on Enemy #{e.Target.Id}");
        }
    }
}
