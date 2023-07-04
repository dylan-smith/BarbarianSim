﻿using BarbarianSim.Enums;

namespace BarbarianSim.Events;

public class DamageEvent : EventInfo
{
    public double Damage { get; set; }
    public DamageType DamageType { get; set; }

    public DamageEvent(double timestamp, double damage, DamageType damageType) : base(timestamp)
    {
        Damage = damage;
        DamageType = damageType;
    }

    public override void ProcessEvent(SimulationState state) => state.Enemy.Life -= (int)Damage;

    public override string ToString() => $"[{Timestamp:F1}] {DamageType} for {Damage:F2}";
}
