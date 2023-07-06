﻿using BarbarianSim.StatCalculators;

namespace BarbarianSim.Events;

public class HealingEvent : EventInfo
{
    public HealingEvent(double timestamp, double baseAmountHealed) : base(timestamp) => BaseAmountHealed = baseAmountHealed;

    public double BaseAmountHealed { get; set; }
    public double AmountHealed { get; set; }
    public double OverHeal { get; set; }

    public override void ProcessEvent(SimulationState state)
    {
        AmountHealed = BaseAmountHealed * HealingReceivedCalculator.Calculate(state);

        if (AmountHealed + state.Player.Life > state.Player.MaxLife)
        {
            OverHeal = state.Player.Life + AmountHealed - state.Player.MaxLife;
            AmountHealed -= OverHeal;
        }

        state.Player.Life += AmountHealed;
    }
}
