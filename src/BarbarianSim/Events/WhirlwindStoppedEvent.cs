using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class WhirlwindStoppedEvent : EventInfo
{
    public WhirlwindStoppedEvent(double timestamp) : base(timestamp)
    {
    }

    public override void ProcessEvent(SimulationState state)
    {
        if (!state.Player.Auras.Remove(Aura.Whirlwinding))
        {
            throw new Exception("Whirlwinding aura was expected in State");
        }
    }
}
