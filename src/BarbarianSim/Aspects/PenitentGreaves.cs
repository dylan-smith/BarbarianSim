using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class PenitentGreaves : Aspect, IHandlesEvent<SimulationStartedEvent>
{
    // You leave behind a trail of frost that Chills enemies. You deal [7 - 10]%[x] more damage to Chilled enemies.
    public int Damage { get; set; }

    public void ProcessEvent(SimulationStartedEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            foreach (var enemy in state.Enemies)
            {
                state.Events.Add(new AuraAppliedEvent(0, "Penitent Greaves", 0, Aura.Chill, enemy));
            }
        }
    }

    public virtual double GetDamageBonus(SimulationState state)
    {
        return IsAspectEquipped(state)
            ? 1 + (Damage / 100.0)
            : 1.0;
    }
}
