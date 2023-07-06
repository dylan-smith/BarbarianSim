using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class ViolentWhirlwindAppliedEvent : EventInfo
{
    public ViolentWhirlwindAppliedEvent(double timestamp) : base(timestamp)
    { }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Add(Aura.ViolentWhirlwind);
    }
}
