using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class AspectOfBerserkRipping : Aspect, IHandlesEvent<DirectDamageEvent>
{
    // Whenever you deal Direct Damage while Berserking inflict 20-30% of the Base Damage dealt as additional Bleeding damage over 5 seconds
    public const double BLEED_DURATION = 5;
    public int Damage { get; set; }

    public void ProcessEvent(DirectDamageEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state) && state.Player.Auras.Contains(Aura.Berserking))
        {
            var bleedAppliedEvent = new BleedAppliedEvent(e.Timestamp, e.BaseDamage * Damage / 100.0, BLEED_DURATION, e.Enemy);
            state.Events.Add(bleedAppliedEvent);
        }
    }
}
