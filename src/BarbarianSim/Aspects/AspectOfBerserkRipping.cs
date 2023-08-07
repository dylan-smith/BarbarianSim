using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfBerserkRipping : Aspect, IHandlesEvent<DirectDamageEvent>
{
    // Whenever you deal Direct Damage while Berserking inflict 20-30% of the Base Damage dealt as additional Bleeding damage over 5 seconds
    public const double BLEED_DURATION = 5;
    public int Damage { get; set; }

    public AspectOfBerserkRipping(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(DirectDamageEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state) && state.Player.Auras.Contains(Aura.Berserking))
        {
            var bleedDamage = e.BaseDamage * Damage / 100.0;
            var bleedAppliedEvent = new BleedAppliedEvent(e.Timestamp, "Aspect of Berserk Ripping", bleedDamage, BLEED_DURATION, e.Enemy);
            state.Events.Add(bleedAppliedEvent);
            _log.Verbose($"Aspect of Berserk Ripping created BleedAppliedEvent for {bleedDamage} damage over {BLEED_DURATION} seconds to Enemy #{e.Enemy.Id}");
        }
    }
}
