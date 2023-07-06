﻿using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class WhirlwindStoppedEvent : EventInfo
{
    public WhirlwindStoppedEvent(double timestamp) : base(timestamp)
    {
    }

    public override void ProcessEvent(SimulationState state)
    {
        state.Player.Auras.Remove(Aura.Whirlwinding);
    }
}
