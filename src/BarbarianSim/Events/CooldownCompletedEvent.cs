using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class CooldownCompletedEvent : EventInfo
{
    public CooldownCompletedEvent(double timestamp, Aura aura) : base(timestamp) => Aura = aura;

    public Aura Aura { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        if (!state.Player.Auras.Remove(Aura))
        {
            throw new Exception($"{Aura} aura was expected in State");
        }
    }
}
