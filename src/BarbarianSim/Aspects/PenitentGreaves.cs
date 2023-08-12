using BarbarianSim.Config;
using BarbarianSim.Enums;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class PenitentGreaves : Aspect, IHandlesEvent<SimulationStartedEvent>
{
    // You leave behind a trail of frost that Chills enemies. You deal [7 - 10]%[x] more damage to Chilled enemies.
    public int Damage { get; set; }

    public PenitentGreaves(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(SimulationStartedEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            foreach (var enemy in state.Enemies)
            {
                state.Events.Add(new AuraAppliedEvent(0, "Penitent Greaves", 0, Aura.Chill, enemy));
                _log.Verbose($"Penitent Greaves created AuraAppliedEvent for Chill to Enemy #{enemy.Id}");
            }
        }
    }

    public virtual double GetDamageBonus(SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            var result = 1 + (Damage / 100.0);
            _log.Verbose($"Penitent Greaves Damage Bonus = {result:F2}x");
            
            return result;
        }

        return 1.0;
    }
}
