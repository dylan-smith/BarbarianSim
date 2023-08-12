using BarbarianSim.Config;
using BarbarianSim.Events;

namespace BarbarianSim.Aspects;

public class RamaladnisMagnumOpus : Aspect, IHandlesEvent<SimulationStartedEvent>
{
    // Skills using this weapon deal [0.1 - 0.3]%[x] increased damage per point of Fury you have, but you lose 2 Fury every second.
    public double DamagePerFury { get; set; }
    public const double FURY_PER_SECOND_LOST = 2.0;

    public RamaladnisMagnumOpus(SimLogger log) => _log = log;

    private readonly SimLogger _log;

    public void ProcessEvent(SimulationStartedEvent e, SimulationState state)
    {
        if (IsAspectEquipped(state))
        {
            state.Events.Add(new RamaladnisMagnumOpusEvent(1.0));
            _log.Verbose($"Ramaladni's Magnum Opus created RamaladnisMagnumOpusEvent at timestamp 1.0");
        }
    }

    public virtual double GetDamageBonus(SimulationState state, GearItem weapon)
    {
        if (weapon?.Aspect == this)
        {
            var result = 1 + (DamagePerFury * state.Player.Fury / 100);

            return result;
        }

        return 1.0;
    }
}
