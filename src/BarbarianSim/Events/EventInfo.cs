﻿namespace BarbarianSim.Events;

public abstract class EventInfo
{
    public double Timestamp { get; set; }

    protected EventInfo(double timestamp) => Timestamp = timestamp;

    public abstract void ProcessEvent(SimulationState state);

    public override string ToString() => $"[{Timestamp:F1}] {GetType().Name}";
}
