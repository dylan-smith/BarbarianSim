using BarbarianSim.Events;
using BarbarianSim.StatCalculators;

namespace BarbarianSim.EventHandlers;

public class HealingEventHandler : EventHandler<HealingEvent>
{
    public override void ProcessEvent(HealingEvent e, SimulationState state)
    {
        e.AmountHealed = e.BaseAmountHealed * HealingReceivedCalculator.Calculate(state);

        if (e.AmountHealed + state.Player.Life > MaxLifeCalculator.Calculate(state))
        {
            e.OverHeal = state.Player.Life + e.AmountHealed - MaxLifeCalculator.Calculate(state);
            e.AmountHealed -= e.OverHeal;
        }

        state.Player.Life += e.AmountHealed;
    }
}
